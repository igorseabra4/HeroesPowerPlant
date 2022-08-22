using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionRenderer
    {
        private SharpMesh quadTreeMesh;
        private DefaultRenderData mainQuadtreeRenderData = new DefaultRenderData();

        public static void SetQuadHeight(float value)
        {
            quadTreeTranslation = Matrix.Translation(0, value, 0);
        }

        private static Matrix quadTreeTranslation = Matrix.Identity;

        public void RenderQuadTree(SharpRenderer renderer)
        {
            if (quadTreeMesh == null)
                return;

            mainQuadtreeRenderData.worldViewProjection = quadTreeTranslation * renderer.viewProjection;

            renderer.Device.SetFillModeSolid();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetDefaultBlendState();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, mainQuadtreeRenderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            quadTreeMesh.Draw(renderer.Device);
        }

        private SharpMesh collisionMesh;
        private CollisionRenderData sceneInformation;

        public void RenderCollisionModel(SharpRenderer renderer)
        {
            if (collisionMesh == null) return;

            sceneInformation = new CollisionRenderData()
            {
                viewProjection = renderer.viewProjection,
                ambientColor = new Vector4(0.1f, 0.1f, 0.3f, 1f),
                lightDirection = new Vector4(-renderer.Camera.GetForward(), 1f),
                lightDirection2 = new Vector4(renderer.Camera.GetUp(), 1f)
            };

            renderer.Device.SetDefaultBlendState();
            renderer.Device.SetCullModeDefault();
            renderer.Device.SetFillModeDefault();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.collisionBuffer, sceneInformation);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.collisionBuffer);
            renderer.collisionShader.Apply();

            collisionMesh.Draw(renderer.Device);
        }

        public void Dispose()
        {
            if (collisionMesh != null)
                collisionMesh.Dispose();
            if (quadTreeMesh != null)
                quadTreeMesh.Dispose();
        }

        public CLFile LoadCLFile(string FileName, SharpDevice device, ProgressBar bar)
        {
            CLFile data = new CLFile(0);

            var reader = new EndianBinaryReader(new FileStream(FileName, FileMode.Open), Endianness.Big);

            reader.BaseStream.Position = 0;

            data.numBytes = reader.ReadUInt32();
            //Get total number of bytes in file

            if (data.numBytes != reader.BaseStream.Length)
                throw new ArgumentException("Not a valid CL file.");

            //Get offset of structs
            data.pointQuadtree = reader.ReadUInt32();
            data.pointTriangle = reader.ReadUInt32();
            data.pointVertex = reader.ReadUInt32();

            //Get quadtree center and radius
            data.quadCenterX = reader.ReadSingle();
            data.quadCenterY = reader.ReadSingle();
            data.quadCenterZ = reader.ReadSingle();
            data.quadLength = reader.ReadSingle();

            //Get amount of stuff
            data.basePower = reader.ReadUInt16();
            data.numTriangles = reader.ReadUInt16();
            data.numVertices = reader.ReadUInt16();
            data.numQuadnodes = reader.ReadUInt16();

            bar.Maximum = data.numTriangles + data.numVertices + 3 * data.numQuadnodes;

            var tList = new List<Triangle>(data.numTriangles);
            var vList = new List<CollisionVertex>(data.numVertices);
            data.MeshTypeList = new List<ulong>();

            for (int i = 0; i < data.numVertices; i++)
            {
                reader.BaseStream.Position = data.pointVertex + i * 0xC;
                vList.Add(new CollisionVertex(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                bar.PerformStep();
            }

            for (int i = 0; i < data.numTriangles; i++)
            {
                reader.BaseStream.Position = data.pointTriangle + i * 0x20;
                tList.Add(new Triangle(reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16()));
                reader.BaseStream.Position += 6;

                var Normals = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                vList[tList[i].Vertices[0]].NormalList.Add(Normals);
                vList[tList[i].Vertices[1]].NormalList.Add(Normals);
                vList[tList[i].Vertices[2]].NormalList.Add(Normals);

                ulong FlagsAsUint64 = reader.ReadUInt64();
                tList[i].ColFlags = BitConverter.GetBytes(FlagsAsUint64);
                if (!data.MeshTypeList.Contains(FlagsAsUint64))
                    data.MeshTypeList.Add(FlagsAsUint64);

                var color = new Color(tList[i].ColFlags[1], tList[i].ColFlags[2], tList[i].ColFlags[3]);

                if (color.R == 0) color.R = 255;
                else
                    color.R = (byte)(256 - (Math.Log(color.R, 2) + 1) * 32);
                if (color.G == 0) color.G = 255;
                else
                    color.G = (byte)(256 - (Math.Log(color.G, 2) + 1) * 32);
                if (color.B == 0) color.B = 255;
                else
                    color.B = (byte)(256 - (Math.Log(color.B, 2) + 1) * 32);

                vList[tList[i].Vertices[0]].Color = color;
                vList[tList[i].Vertices[1]].Color = color;
                vList[tList[i].Vertices[2]].Color = color;

                bar.PerformStep();
            }

            data.CLTriangleArray = tList.ToArray();

            for (int i = 0; i < data.numQuadnodes; i++)
            {
                reader.BaseStream.Position = data.pointQuadtree + i * 0x20;
                QuadNode TempNode = new QuadNode
                {
                    Index = reader.ReadUInt16(),
                    Parent = reader.ReadUInt16(),
                    Child = reader.ReadUInt16()
                };
                reader.BaseStream.Position += 8;
                TempNode.NodeTriangleAmount = reader.ReadUInt16();
                TempNode.TriListOffset = reader.ReadUInt32();
                TempNode.PosValueX = reader.ReadUInt16();
                TempNode.PosValueZ = reader.ReadUInt16();
                TempNode.Depth = reader.ReadByte();

                data.CLQuadNodeList.Add(TempNode);
                bar.PerformStep();
            }

            ReBuildQuadtree(ref data, bar);
            DetermineQuadtreeRenderStuff(ref data, device, bar);

            reader.Close();

            data.CLVertexArray = new VertexColoredNormalized[vList.Count()];
            for (int i = 0; i < vList.Count; i++)
            {
                data.CLVertexArray[i] = new VertexColoredNormalized(vList[i].Position,
                    vList[i].CalculateNormals(),
                    vList[i].Color);
            }

            int[] IndexArray = new int[data.CLTriangleArray.Count() * 3];

            for (int i = 0; i < data.CLTriangleArray.Count() * 3; i++)
                IndexArray[i] = data.CLTriangleArray[i / 3].Vertices[i % 3];

            if (collisionMesh != null)
                collisionMesh.Dispose();

            collisionMesh = SharpMesh.Create(device, data.CLVertexArray, IndexArray, new List<SharpSubSet>() { new SharpSubSet(0, IndexArray.Count(), null) }, SharpDX.Direct3D.PrimitiveTopology.TriangleList);

            return data;
        }

        public bool ReBuildQuadtree(ref CLFile data, ProgressBar bar)
        {
            data.CLQuadNodeList[0].NodeSquare.X = data.quadCenterX - (data.quadLength / 2);
            data.CLQuadNodeList[0].NodeSquare.Y = data.quadCenterZ - (data.quadLength / 2);

            data.CLQuadNodeList[0].NodeSquare.Height = data.quadLength;
            data.CLQuadNodeList[0].NodeSquare.Width = data.quadLength;

            for (int i = 0; i < data.CLQuadNodeList.Count; i++)
            {
                if (data.CLQuadNodeList[i].Child != 0)
                {
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 0);
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 1);
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 2);
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 3);
                }
                bar.PerformStep();
            }

            return true;
        }

        public void GiveSquareToChild(ref CLFile data, RectangleF r, int Child, int Offset)
        {
            data.CLQuadNodeList[Child + Offset].NodeSquare = new RectangleF(r.X, r.Y, r.Width / 2, r.Height / 2);

            if (Offset == 1)
                data.CLQuadNodeList[Child + Offset].NodeSquare.X += data.CLQuadNodeList[Child + Offset].NodeSquare.Width;
            else if (Offset == 2)
                data.CLQuadNodeList[Child + Offset].NodeSquare.Y += data.CLQuadNodeList[Child + Offset].NodeSquare.Height;
            else if (Offset == 3)
            {
                data.CLQuadNodeList[Child + Offset].NodeSquare.X += data.CLQuadNodeList[Child + Offset].NodeSquare.Width;
                data.CLQuadNodeList[Child + Offset].NodeSquare.Y += data.CLQuadNodeList[Child + Offset].NodeSquare.Height;
            }
        }

        public void DetermineQuadtreeRenderStuff(ref CLFile data, SharpDevice device, ProgressBar bar)
        {
            List<Vertex> QuadNodeVertexList = new List<Vertex>();
            List<int> QuadNodeIndexList = new List<int>();

            int k = 0;
            foreach (QuadNode i in data.CLQuadNodeList)
            {
                bar.PerformStep();
                if (i.Child == 0 | i.Index == 0)
                {
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 0, i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k);
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 0, i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 0, i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 0, i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeIndexList.Add(k);
                    k += 4;
                }
            }

            if (quadTreeMesh != null)
                quadTreeMesh.Dispose();

            quadTreeMesh = SharpMesh.Create(device, QuadNodeVertexList.ToArray(), QuadNodeIndexList.ToArray(), new List<SharpSubSet>(){
                new SharpSubSet(0, QuadNodeIndexList.Count, null) }, SharpDX.Direct3D.PrimitiveTopology.LineList);
            mainQuadtreeRenderData.Color = new Vector4(0.9f, 0.3f, 0.6f, 1f);
        }
    }
}
