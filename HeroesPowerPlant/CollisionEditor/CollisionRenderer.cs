using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharpDX;
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
            
            BinaryReader CLReader = new BinaryReader(new FileStream(FileName, FileMode.Open));

            CLReader.BaseStream.Position = 0;

            data.numBytes = Switch(CLReader.ReadUInt32());
            //Get total number of bytes in file

            if (data.numBytes != CLReader.BaseStream.Length)
                throw new ArgumentException("Not a valid CL file.");

            //Get offset of structs
            data.pointQuadtree = Switch(CLReader.ReadUInt32());
            data.pointTriangle = Switch(CLReader.ReadUInt32());
            data.pointVertex = Switch(CLReader.ReadUInt32());

            //Get quadtree center and radius
            data.quadCenterX = Switch(CLReader.ReadSingle());
            data.quadCenterY = Switch(CLReader.ReadSingle());
            data.quadCenterZ = Switch(CLReader.ReadSingle());
            data.quadLenght = Switch(CLReader.ReadSingle());

            //Get amount of stuff
            data.basePower = Switch(CLReader.ReadUInt16());
            data.numTriangles = Switch(CLReader.ReadUInt16());
            data.numVertices = Switch(CLReader.ReadUInt16());
            data.numQuadnodes = Switch(CLReader.ReadUInt16());

            bar.Maximum = data.numTriangles + data.numVertices + 3 * data.numQuadnodes;

            List<Triangle> CLTriangleList = new List<Triangle>(data.numTriangles);
            List<CollisionVertex> CLVertexList = new List<CollisionVertex>(data.numVertices);
            data.MeshTypeList = new List<UInt64>();

            for (int i = 0; i < data.numVertices; i++)
            {
                CLReader.BaseStream.Position = data.pointVertex + i * 0xC;
                CLVertexList.Add(new CollisionVertex(Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle())));
                bar.PerformStep();
            }
            
            for (int i = 0; i < data.numTriangles; i++)
            {
                CLReader.BaseStream.Position = data.pointTriangle + i * 0x20;
                CLTriangleList.Add(new Triangle(Switch(CLReader.ReadUInt16()), Switch(CLReader.ReadUInt16()), Switch(CLReader.ReadUInt16())));
                CLReader.BaseStream.Position += 6;

                Vector3 Normals = new Vector3(Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()));
                CLVertexList[CLTriangleList[i].Vertices[0]].NormalList.Add(Normals);
                CLVertexList[CLTriangleList[i].Vertices[1]].NormalList.Add(Normals);
                CLVertexList[CLTriangleList[i].Vertices[2]].NormalList.Add(Normals);

                UInt64 FlagsAsUint64 = CLReader.ReadUInt64();
                CLTriangleList[i].ColFlags = BitConverter.GetBytes(FlagsAsUint64);
                if (!data.MeshTypeList.Contains(FlagsAsUint64))
                    data.MeshTypeList.Add(FlagsAsUint64);

                Color TempColor = new Color(CLTriangleList[i].ColFlags[1], CLTriangleList[i].ColFlags[2], CLTriangleList[i].ColFlags[3]);

                if (TempColor.R == 0) TempColor.R = 255;
                else
                    TempColor.R = (byte)(256 - (Math.Log(TempColor.R, 2) + 1) * 32);
                if (TempColor.G == 0) TempColor.G = 255;
                else
                    TempColor.G = (byte)(256 - (Math.Log(TempColor.G, 2) + 1) * 32);
                if (TempColor.B == 0) TempColor.B = 255;
                else
                    TempColor.B = (byte)(256 - (Math.Log(TempColor.B, 2) + 1) * 32);
                
                CLVertexList[CLTriangleList[i].Vertices[0]].Color = TempColor;
                CLVertexList[CLTriangleList[i].Vertices[1]].Color = TempColor;
                CLVertexList[CLTriangleList[i].Vertices[2]].Color = TempColor;

                bar.PerformStep();
            }

            data.CLTriangleArray = CLTriangleList.ToArray();

            for (int i = 0; i < data.numQuadnodes; i++)
            {
                CLReader.BaseStream.Position = data.pointQuadtree + i * 0x20;
                QuadNode TempNode = new QuadNode
                {
                    Index = Switch(CLReader.ReadUInt16()),
                    Parent = Switch(CLReader.ReadUInt16()),
                    Child = Switch(CLReader.ReadUInt16())
                };
                CLReader.BaseStream.Position += 8;
                TempNode.NodeTriangleAmount = Switch(CLReader.ReadUInt16());
                TempNode.TriListOffset = Switch(CLReader.ReadUInt32());
                TempNode.PosValueX = Switch(CLReader.ReadUInt16());
                TempNode.PosValueZ = Switch(CLReader.ReadUInt16());
                TempNode.Depth = CLReader.ReadByte();

                data.CLQuadNodeList.Add(TempNode);
                bar.PerformStep();
            }

            ReBuildQuadtree(ref data, bar);
            DetermineQuadtreeRenderStuff(ref data, device, bar);

            CLReader.Close();

            data.CLVertexArray = new VertexColoredNormalized[CLVertexList.Count()];
            for (int i = 0; i < CLVertexList.Count; i++)
            {
                data.CLVertexArray[i] = new VertexColoredNormalized(CLVertexList[i].Position,
                    CLVertexList[i].CalculateNormals(),
                    CLVertexList[i].Color);
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
            data.CLQuadNodeList[0].NodeSquare.X = data.quadCenterX - (data.quadLenght / 2);
            data.CLQuadNodeList[0].NodeSquare.Y = data.quadCenterZ - (data.quadLenght / 2);

            data.CLQuadNodeList[0].NodeSquare.Height = data.quadLenght;
            data.CLQuadNodeList[0].NodeSquare.Width = data.quadLenght;

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
