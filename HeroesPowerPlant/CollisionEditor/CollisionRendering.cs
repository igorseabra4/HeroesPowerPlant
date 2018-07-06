using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.Collision
{
    public partial class CollisionFunctions
    {
        private static SharpMesh quadTreeMesh;
        private static DefaultRenderData mainQuadtreeRenderData = new DefaultRenderData();

        public static void SetQuadHeight(float value)
        {
            quadTreeTranslation = Matrix.Translation(0, value, 0);
        }

        private static Matrix quadTreeTranslation = Matrix.Identity;

        public static void RenderQuadTree(Matrix viewProjection)
        {
            if (quadTreeMesh == null)
                return;

            mainQuadtreeRenderData.worldViewProjection = quadTreeTranslation * viewProjection;

            device.SetFillModeSolid();
            device.SetCullModeNone();
            device.SetDefaultBlendState();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, mainQuadtreeRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            quadTreeMesh.Draw();
        }

        private static SharpMesh secondQuadTreeMesh;
        private static DefaultRenderData secondQuadtreeRenderData = new DefaultRenderData();

        public static void RenderQuadTree2(SharpDevice device, SharpShader shader, SharpDX.Direct3D11.Buffer buffer, Matrix viewProjection)
        {
            if (secondQuadTreeMesh == null)
                return;

            secondQuadtreeRenderData.worldViewProjection = quadTreeTranslation * viewProjection;

            device.SetFillModeSolid();
            device.SetCullModeNone();
            device.SetDefaultBlendState();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(buffer, secondQuadtreeRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, buffer);
            shader.Apply();

            secondQuadTreeMesh.Draw();
        }

        private static SharpMesh collisionMesh;
        private static CollisionRenderData sceneInformation;

        public static void RenderCollisionModel(Matrix viewProjection, Vector3 lightDirection, Vector3 lightDirection2)
        {
            if (collisionMesh == null) return;

            sceneInformation = new CollisionRenderData()
            {
                viewProjection = viewProjection,
                ambientColor = new Vector4(0.1f, 0.1f, 0.3f, 1f),
                lightDirection = new Vector4(lightDirection, 1f),
                lightDirection2 = new Vector4(lightDirection2, 1f)
            };

            device.SetDefaultBlendState();
            device.SetCullModeDefault();
            device.SetFillModeDefault();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(collisionBuffer, sceneInformation);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, collisionBuffer);
            collisionShader.Apply();

            collisionMesh.Draw();
        }
        
        public static bool LoadCLFile(string FileName)
        {
            if (FileName == "")
                return false;

            UseHeader = new Header();
            CLQuadNodeList.Clear();

            BinaryReader CLReader = new BinaryReader(new FileStream(FileName, FileMode.Open));

            CLReader.BaseStream.Position = 0;

            UseHeader.numBytes = Switch(CLReader.ReadUInt32());
            //Get total number of bytes in file

            if (UseHeader.numBytes != CLReader.BaseStream.Length)
                return false;

            //Get offset of structs
            UseHeader.pointQuadtree = Switch(CLReader.ReadUInt32());
            UseHeader.pointTriangle = Switch(CLReader.ReadUInt32());
            UseHeader.pointVertex = Switch(CLReader.ReadUInt32());

            //Get quadtree center and radius
            UseHeader.quadCenterX = Switch(CLReader.ReadSingle());
            UseHeader.quadCenterY = Switch(CLReader.ReadSingle());
            UseHeader.quadCenterZ = Switch(CLReader.ReadSingle());
            UseHeader.quadLenght = Switch(CLReader.ReadSingle());

            //Get amount of stuff
            UseHeader.PowerFlag = Switch(CLReader.ReadUInt16());
            UseHeader.numTriangles = Switch(CLReader.ReadUInt16());
            UseHeader.numVertices = Switch(CLReader.ReadUInt16());
            UseHeader.numQuadnodes = Switch(CLReader.ReadUInt16());

            Program.collisionEditor.progressBar1.Maximum = UseHeader.numTriangles + UseHeader.numVertices + 3 * UseHeader.numQuadnodes;

            List<CLTriangle> CLTriangleList = new List<CLTriangle>(UseHeader.numTriangles);
            CLVertexList = new List<CLVertex>(UseHeader.numVertices);
            MeshTypeList = new List<UInt64>();

            Program.collisionEditor.label3.Text = "Number of Vertices: " + UseHeader.numVertices.ToString();
            Program.collisionEditor.label4.Text = "Number of Triangles: " + UseHeader.numTriangles.ToString();
            Program.collisionEditor.label5.Text = "Number of QuadNodes: " + UseHeader.numQuadnodes.ToString();

            for (int i = 0; i < UseHeader.numVertices; i++)
            {
                CLReader.BaseStream.Position = UseHeader.pointVertex + i * 0xC;
                CLVertexList.Add(new CLVertex(Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle())));
                Program.collisionEditor.progressBar1.PerformStep();
            }
            
            for (int i = 0; i < UseHeader.numTriangles; i++)
            {
                CLReader.BaseStream.Position = UseHeader.pointTriangle + i * 0x20;
                CLTriangleList.Add(new CLTriangle(Switch(CLReader.ReadUInt16()), Switch(CLReader.ReadUInt16()), Switch(CLReader.ReadUInt16())));
                CLReader.BaseStream.Position += 6;

                Vector3 Normals = new Vector3(Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()));
                CLVertexList[CLTriangleList[i].Vertices[0]].NormalList.Add(Normals);
                CLVertexList[CLTriangleList[i].Vertices[1]].NormalList.Add(Normals);
                CLVertexList[CLTriangleList[i].Vertices[2]].NormalList.Add(Normals);

                UInt64 FlagsAsUint64 = CLReader.ReadUInt64();
                CLTriangleList[i].ColFlags = BitConverter.GetBytes(FlagsAsUint64);
                if (!MeshTypeList.Contains(FlagsAsUint64))
                    MeshTypeList.Add(FlagsAsUint64);

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

                Program.collisionEditor.progressBar1.PerformStep();
            }

            CLTriangleArray = CLTriangleList.ToArray();

            for (int i = 0; i < UseHeader.numQuadnodes; i++)
            {
                CLReader.BaseStream.Position = UseHeader.pointQuadtree + i * 0x20;
                CLQuadNode TempNode = new CLQuadNode
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

                CLQuadNodeList.Add(TempNode);
                Program.collisionEditor.progressBar1.PerformStep();
            }

            ReBuildQuadtree();
            DetermineQuadtreeRenderStuff();

            CLReader.Close();

            CLVertexArray = new VertexColoredNormalized[CLVertexList.Count()];
            for (int i = 0; i < CLVertexList.Count; i++)
            {
                CLVertexArray[i] = new VertexColoredNormalized(CLVertexList[i].Position,
                    CLVertexList[i].CalculateNormals(),
                    CLVertexList[i].Color);
            }

            int[] IndexArray = new int[CLTriangleArray.Count() * 3];

            for (int i = 0; i < CLTriangleArray.Count() * 3; i++)
                IndexArray[i] = CLTriangleArray[i / 3].Vertices[i % 3];

            collisionMesh = SharpMesh.Create(SharpRenderer.device, CLVertexArray, IndexArray, new List<SharpSubSet>() { new SharpSubSet(0, IndexArray.Count(), null) }, SharpDX.Direct3D.PrimitiveTopology.TriangleList);

            return true;
        }

        public static bool ReBuildQuadtree()
        {
            CLQuadNodeList[0].NodeSquare.X = UseHeader.quadCenterX - (UseHeader.quadLenght / 2);
            CLQuadNodeList[0].NodeSquare.Y = UseHeader.quadCenterZ - (UseHeader.quadLenght / 2);

            CLQuadNodeList[0].NodeSquare.Height = UseHeader.quadLenght;
            CLQuadNodeList[0].NodeSquare.Width = UseHeader.quadLenght;

            for (int i = 0; i < CLQuadNodeList.Count; i++)
            {
                if (CLQuadNodeList[i].Child != 0)
                {
                    GiveSquareToChild(CLQuadNodeList[i].NodeSquare, CLQuadNodeList[i].Child, 0);
                    GiveSquareToChild(CLQuadNodeList[i].NodeSquare, CLQuadNodeList[i].Child, 1);
                    GiveSquareToChild(CLQuadNodeList[i].NodeSquare, CLQuadNodeList[i].Child, 2);
                    GiveSquareToChild(CLQuadNodeList[i].NodeSquare, CLQuadNodeList[i].Child, 3);
                }
                Program.collisionEditor.progressBar1.PerformStep();
            }

            //sewer's code
            Program.collisionEditor.checkedListBox1.Items.Clear();
            foreach (CLQuadNode i in CLQuadNodeList)
                Program.collisionEditor.checkedListBox1.Items.Add(i);
            // end

            return true;
        }

        public static void GiveSquareToChild(RectangleF r, int Child, int Offset)
        {
            CLQuadNodeList[Child + Offset].NodeSquare = new RectangleF(r.X, r.Y, r.Width / 2, r.Height / 2);

            if (Offset == 1)
                CLQuadNodeList[Child + Offset].NodeSquare.X += CLQuadNodeList[Child + Offset].NodeSquare.Width;
            else if (Offset == 2)
                CLQuadNodeList[Child + Offset].NodeSquare.Y += CLQuadNodeList[Child + Offset].NodeSquare.Height;
            else if (Offset == 3)
            {
                CLQuadNodeList[Child + Offset].NodeSquare.X += CLQuadNodeList[Child + Offset].NodeSquare.Width;
                CLQuadNodeList[Child + Offset].NodeSquare.Y += CLQuadNodeList[Child + Offset].NodeSquare.Height;
            }
        }
        
        public static void DetermineQuadtreeRenderStuff()
        {
            List<Vertex> QuadNodeVertexList = new List<Vertex>();
            List<Int32> QuadNodeIndexList = new List<Int32>();

            Int32 k = 0;
            foreach (CLQuadNode i in CLQuadNodeList)
            {
                Program.collisionEditor.progressBar1.PerformStep();
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

            quadTreeMesh = SharpMesh.Create(SharpRenderer.device, QuadNodeVertexList.ToArray(), QuadNodeIndexList.ToArray(), new List<SharpSubSet>(){
                new SharpSubSet(0, QuadNodeIndexList.Count, null) }, SharpDX.Direct3D.PrimitiveTopology.LineList);
            mainQuadtreeRenderData.Color = new Vector4(0.9f, 0.3f, 0.6f, 1f);
        }
        
        //sewer's code
        public static void DetermineRenderStuff2()
        {
            List<Vertex> QuadNodeVertexList = new List<Vertex>();
            List<Int32> QuadNodeIndexList = new List<Int32>();

            Int32 k = 0;
            foreach (CLQuadNode i in CLQuadNodeList)
            {
                if (Program.collisionEditor.checkedListBox1.CheckedIndices.Contains(i.Index))
                {
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 100 / (i.Depth + 1), i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k);
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 100 / (i.Depth + 1), i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 100 / (i.Depth + 1), i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 100 / (i.Depth + 1), i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeIndexList.Add(k);
                    k += 4;
                }
            }
            
            secondQuadTreeMesh = SharpMesh.Create(SharpRenderer.device, QuadNodeVertexList.ToArray(), QuadNodeIndexList.ToArray(), new List<SharpSubSet>(){
                new SharpSubSet(0, QuadNodeIndexList.Count, null) }, SharpDX.Direct3D.PrimitiveTopology.LineList);
            secondQuadtreeRenderData.Color = new Vector4(0.3f, 0.9f, 0.6f, 1f);
        }
    }
}
