using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionSystem
    {
        public string CurrentCLfileName;
        public int NumVertices => data.numVertices;
        public int NumTriangles => data.numTriangles;
        public int NumQuadNodes => data.numQuadnodes;
        public byte DepthLevel => data.MaxDepth;

        private CLFile data;
        private CollisionRenderer collisionRenderer;

        public CollisionSystem()
        {
            collisionRenderer = new CollisionRenderer();
        }

        public void Render(SharpRenderer renderer)
        {
            collisionRenderer.RenderCollisionModel(renderer);
        }

        public void RenderQuadtree(SharpRenderer renderer)
        {
            collisionRenderer.RenderQuadTree(renderer);
        }

        public void Dispose()
        {
            collisionRenderer.Dispose();
        }

        public void Import(string sourceOBJfile, ushort basePower, byte depthLevel, bool flipNormals, ProgressBar bar)
        {
            ConvertOBJtoCL(sourceOBJfile, CurrentCLfileName, basePower, depthLevel, flipNormals, bar);
        }

        public void NewFile(string sourceOBJfile, string destinationCLfile, ushort basePower, byte depthLevel, bool flipNormals, ProgressBar bar)
        {
            CurrentCLfileName = destinationCLfile;
            Import(sourceOBJfile, basePower, depthLevel, flipNormals, bar);
        }

        public void Open(string fileName)
        {
            CurrentCLfileName = fileName;
        }

        public void ConvertCLtoOBJ(string fileName)
        {
            ConvertCLtoOBJ(fileName, ref data);
        }

        public void Close()
        {
            CurrentCLfileName = null;
            collisionRenderer.Dispose();
        }

        public bool HasOpenedFile()
        {
            return CurrentCLfileName != null;
        }

        public void LoadCLFile(SharpDevice device, ProgressBar bar)
        {
            collisionRenderer.Dispose();

            data = collisionRenderer.LoadCLFile(CurrentCLfileName, device, bar);
        }

        public void ConvertOBJtoCL(string InputFile, string OutputFile, ushort basePower, byte depthLevel, bool flipNormals, ProgressBar bar)
        {
            CLFile data = new CLFile(depthLevel);

            bar.Minimum = 0;
            bar.Value = 0;
            bar.Step = 1;

            if (ReadOBJFile(InputFile, ref data, flipNormals, bar))
                if (GenerateCollision(ref data, basePower, bar))
                    CreateCLFile(OutputFile, ref data, bar);
        }

        public bool ReadOBJFile(string InputFile, ref CLFile data, bool flipNormals, ProgressBar bar)
        {
            string[] OBJFile = File.ReadAllLines(InputFile);
            bar.Maximum = 65535 + OBJFile.Length;

            int CurrentMeshNum = -1;
            byte[] TempColFlags = { 0, 0, 0, 0x0 };

            List<Triangle> CLTriangleList = new List<Triangle>(65535);
            List<CollisionVertex> CLVertexList = new List<CollisionVertex>(65535);

            bool lastadded = true;

            foreach (string j in OBJFile)
            {
                if (j.StartsWith("v "))
                {
                    string a = Regex.Replace(j, @"\s+", " ");
                    string[] SubStrings = a.Split(' ');
                    CLVertexList.Add(new CollisionVertex(Convert.ToSingle(SubStrings[1]), Convert.ToSingle(SubStrings[2]), Convert.ToSingle(SubStrings[3])));
                    bar.PerformStep();
                }
                else if (j.StartsWith("f "))
                {
                    string[] SubStrings = j.Split(' ');
                    CLTriangleList.Add(new Triangle(
                    (ushort)(Convert.ToUInt16(SubStrings[1].Split('/')[0]) - 1),
                    (ushort)(Convert.ToUInt16(SubStrings[2].Split('/')[0]) - 1),
                    (ushort)(Convert.ToUInt16(SubStrings[3].Split('/')[0]) - 1),
                    CurrentMeshNum, TempColFlags, CLVertexList, flipNormals));
                    bar.PerformStep();

                    lastadded = true;
                }
                else if (j.StartsWith("g ") || j.StartsWith("o "))
                {
                    if (lastadded)
                        CurrentMeshNum += 1;

                    lastadded = false;

                    TempColFlags = new byte[] { 0, 0, 0, 0 };

                    if (j.Contains('_'))
                    {
                        if (j.Split('_').Last().Count() == 8)
                        {
                            try
                            {
                                string a = j.Split('_').Last();
                                TempColFlags[0] = Convert.ToByte(new string(new char[] { a[0], a[1] }), 16);
                                TempColFlags[1] = Convert.ToByte(new string(new char[] { a[2], a[3] }), 16);
                                TempColFlags[2] = Convert.ToByte(new string(new char[] { a[4], a[5] }), 16);
                                TempColFlags[3] = Convert.ToByte(new string(new char[] { a[6], a[7] }), 16);
                            }
                            catch
                            {
                                TempColFlags = new byte[] { 0, 0, 0, 0 };
                            }
                        }
                        else
                        {
                            if (j.Split('_').Last().Contains("b")) //bingo
                                TempColFlags[0] = 0x40;
                            else if (j.Split('_').Last().Contains("p")) //pinball
                                TempColFlags[0] = 0x80;
                            else if (j.Split('_').Last().Contains("x")) //death
                                TempColFlags[1] = 0x01;
                            else if (j.Split('_').Last().Contains("l")) //slippery
                                TempColFlags[1] = 0x04;
                            else if (j.Split('_').Last().Contains("t")) //triangle jump
                                TempColFlags[1] = 0x08;
                            else if (j.Split('_').Last().Contains("w")) //wall
                                TempColFlags[2] = 0x01;
                            else if (j.Split('_').Last().Contains("s")) //stairs
                                TempColFlags[2] = 0x04;
                            else if (j.Split('_').Last().Contains("k")) //barrier
                                TempColFlags[2] = 0x80;
                            else if (j.Split('_').Last().Contains("i")) // invisible wall
                            {
                                TempColFlags[2] = 0x01;
                                TempColFlags[3] = 0x80;
                            }
                            else if (j.Split('_').Last().Contains("a")) // water
                            {
                                TempColFlags[3] = 0x02;
                            }
                        }
                    }
                }
            }

            bar.Maximum = 65535 + CLVertexList.Count() + CLTriangleList.Count();

            if (CLVertexList.Count >= 0xffff)
            {
                MessageBox.Show("Error: Maximum amount of 65535 vertices reached.");
                return false;
            }

            if (CLTriangleList.Count >= 0xffff)
            {
                MessageBox.Show("Error: Maximum amount of 65535 triangles reached.");
                return false;
            }

            data.CLVertexArray = new VertexColoredNormalized[CLVertexList.Count()];
            for (int i = 0; i < CLVertexList.Count; i++)
            {
                data.CLVertexArray[i] = new VertexColoredNormalized(CLVertexList[i].Position,
                    CLVertexList[i].CalculateNormals(),
                    CLVertexList[i].Color);
            }

            data.CLTriangleArray = CLTriangleList.ToArray();
            return true;
        }

        public bool GenerateCollision(ref CLFile data, ushort basePower, ProgressBar bar)
        {
            //Let's start with quadtree maximums, minimums and center
            float MaxX = data.CLVertexArray[0].Position.X;
            float MaxY = data.CLVertexArray[0].Position.Y;
            float MaxZ = data.CLVertexArray[0].Position.Z;
            float MinX = data.CLVertexArray[0].Position.X;
            float MinY = data.CLVertexArray[0].Position.Y;
            float MinZ = data.CLVertexArray[0].Position.Z;

            foreach (VertexColoredNormalized i in data.CLVertexArray)
            {
                if (i.Position.X > MaxX)
                    MaxX = i.Position.X;
                if (i.Position.Y > MaxY)
                    MaxY = i.Position.Y;
                if (i.Position.Z > MaxZ)
                    MaxZ = i.Position.Z;
                if (i.Position.X < MinX)
                    MinX = i.Position.X;
                if (i.Position.Y < MinY)
                    MinY = i.Position.Y;
                if (i.Position.Z < MinZ)
                    MinZ = i.Position.Z;
            }

            data.quadCenterX = (MaxX + MinX) / 2.0f;
            data.quadCenterY = (MaxY + MinY) / 2.0f;
            data.quadCenterZ = (MaxZ + MinZ) / 2.0f;

            data.quadLength = Math.Max(MaxX - MinX, MaxZ - MinZ);

            data.quadLength = (float)Math.Ceiling(data.quadLength);

            if (data.quadLength % 16 != 0)
                data.quadLength = (((int)(data.quadLength) / 16) + 1) * 16;

            if (data.MaxDepth == 0)
            {
                data.MaxDepth = (byte)(Math.Log(data.quadLength / 50) / (Math.Log(2)));
                if (data.MaxDepth > 10)
                    data.MaxDepth = 10;
            }

            data.numTriangles = (ushort)data.CLTriangleArray.Count();
            data.numVertices = (ushort)data.CLVertexArray.Count();

            //Now let's build the quadtree
            data.basePower = basePower;

            if (BuildQuadtree(ref data, bar))
            {
                data.numQuadnodes = (ushort)data.CLQuadNodeList.Count;
                return true;
            }

            return false;
        }

        public bool BuildQuadtree(ref CLFile data, ProgressBar bar)
        {
            QuadNode TempNode = new QuadNode();

            TempNode.NodeSquare.X = data.quadCenterX - (data.quadLength / 2);
            TempNode.NodeSquare.Y = data.quadCenterZ - (data.quadLength / 2);
            TempNode.NodeSquare.Width = data.quadLength;
            TempNode.NodeSquare.Height = data.quadLength;

            TempNode.Child = 1;
            TempNode.NodeTriangleArray = Range((ushort)data.CLTriangleArray.Count());

            data.CLQuadNodeList.Add(TempNode);
            bar.PerformStep();

            int i = 0;

            while (i < data.CLQuadNodeList.Count)
            {
                if (data.CLQuadNodeList[i].Depth != data.MaxDepth & data.CLQuadNodeList[i].NodeTriangleArray.Count() > 0)
                {
                    data.CLQuadNodeList[i].Child = (ushort)data.CLQuadNodeList.Count;

                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 0, (ushort)data.CLQuadNodeList.Count, data.basePower, data.CLTriangleArray));
                    bar.PerformStep();
                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 1, (ushort)data.CLQuadNodeList.Count, data.basePower, data.CLTriangleArray));
                    bar.PerformStep();
                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 2, (ushort)data.CLQuadNodeList.Count, data.basePower, data.CLTriangleArray));
                    bar.PerformStep();
                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 3, (ushort)data.CLQuadNodeList.Count, data.basePower, data.CLTriangleArray));
                    bar.PerformStep();

                    data.CLQuadNodeList[i].NodeTriangleArray = null;
                    data.CLQuadNodeList[i].NodeTriangleAmount = 0;
                }
                i += 1;
            }

            bar.Maximum = 2 * data.CLVertexArray.Count() + 2 * data.CLTriangleArray.Count() + 4 * data.CLQuadNodeList.Count();
            return true;
        }

        public QuadNode CreateNode(QuadNode NodeParent, byte NodeOrient, ushort Count, int PowerFlag, Triangle[] CLTriangleArray)
        {
            QuadNode NodeChild = new QuadNode
            {
                Index = Count,
                Parent = NodeParent.Index,
                Child = 0,
                Depth = (byte)(NodeParent.Depth + 1),

                PosValueX = NodeParent.PosValueX,
                PosValueZ = NodeParent.PosValueZ,

                NodeSquare = new SharpDX.RectangleF(NodeParent.NodeSquare.X, NodeParent.NodeSquare.Y, NodeParent.NodeSquare.Width / 2f, NodeParent.NodeSquare.Height / 2f)
            };

            ushort offsetValue = (ushort)Math.Pow(2, (PowerFlag - NodeChild.Depth));

            if (NodeOrient == 1)
            {
                NodeChild.PosValueX += offsetValue;
                NodeChild.NodeSquare.X += NodeChild.NodeSquare.Width;
            }
            else if (NodeOrient == 2)
            {
                NodeChild.PosValueZ += offsetValue;
                NodeChild.NodeSquare.Y += NodeChild.NodeSquare.Height;
            }
            else if (NodeOrient == 3)
            {
                NodeChild.PosValueX += offsetValue;
                NodeChild.NodeSquare.X += NodeChild.NodeSquare.Width;
                NodeChild.PosValueZ += offsetValue;
                NodeChild.NodeSquare.Y += NodeChild.NodeSquare.Height;
            }

            NodeChild.NodeTriangleArray = GetTrianglesInsideNode(NodeChild, NodeParent.NodeTriangleArray, CLTriangleArray);
            NodeChild.NodeTriangleAmount = (ushort)NodeChild.NodeTriangleArray.Count();

            return NodeChild;
        }

        public ushort[] GetTrianglesInsideNode(QuadNode Node, ushort[] TriangleList, Triangle[] CLTriangleArray)
        {
            List<ushort> NodeTriangleList = new List<ushort>();

            foreach (ushort i in TriangleList)
                if (Node.NodeSquare.Intersects(CLTriangleArray[i].TasRect))
                    NodeTriangleList.Add(i);

            if (NodeTriangleList.Count != 0)
                while (NodeTriangleList.Count < 3)
                    NodeTriangleList.Add(0);

            return NodeTriangleList.ToArray();
        }

        public bool CreateCLFile(string fileName, ref CLFile data, ProgressBar bar)
        {
            //Finally, let's write the file
            using var writer = new EndianBinaryWriter(new MemoryStream(
                0x20 * data.CLQuadNodeList.Count() +
                0x20 * data.CLTriangleArray.Count() +
                0xC * data.CLVertexArray.Count() + 0x28), Endianness.Big);

            writer.BaseStream.Position = 0x28;
            for (ushort i = 0; i < data.CLQuadNodeList.Count; i++)
            {
                if ((data.CLQuadNodeList[i].Depth == data.MaxDepth) & (data.CLQuadNodeList[i].NodeTriangleAmount > 0))
                {
                    data.CLQuadNodeList[i].TriListOffset = (uint)writer.BaseStream.Position;
                    foreach (var j in data.CLQuadNodeList[i].NodeTriangleArray)
                        writer.Write(j);
                }
                bar.PerformStep();
            }

            if (writer.BaseStream.Position % 4 == 2)
                writer.Write((ushort)0);

            data.pointQuadtree = (uint)writer.BaseStream.Position;

            foreach (QuadNode i in data.CLQuadNodeList)
            {
                writer.Write(i.Index);
                writer.Write(i.Parent);
                writer.Write(i.Child);
                writer.Write((ushort)0);
                writer.Write((ushort)0);
                writer.Write((ushort)0);
                writer.Write((ushort)0);
                writer.Write(i.NodeTriangleAmount);
                writer.Write(i.TriListOffset);
                writer.Write(i.PosValueX);
                writer.Write(i.PosValueZ);
                writer.Write(i.Depth);
                writer.Write((byte)0);
                writer.Write((ushort)0);
                writer.Write(0);

                bar.PerformStep();
            }

            data.pointTriangle = (uint)writer.BaseStream.Position;

            foreach (var i in data.CLTriangleArray)
            {
                writer.Write(i.Vertices[0]);
                writer.Write(i.Vertices[1]);
                writer.Write(i.Vertices[2]);
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write(i.Normals.X);
                writer.Write(i.Normals.Y);
                writer.Write(i.Normals.Z);
                writer.Write(i.ColFlags[0]);
                writer.Write(i.ColFlags[1]);
                writer.Write(i.ColFlags[2]);
                writer.Write(i.ColFlags[3]);
                writer.Write(i.MeshNum);
                writer.Write((ushort)0);

                bar.PerformStep();
            }

            data.pointVertex = (uint)writer.BaseStream.Position;

            foreach (var i in data.CLVertexArray)
            {
                writer.Write(i.Position.X);
                writer.Write(i.Position.Y);
                writer.Write(i.Position.Z);

                bar.PerformStep();
            }

            data.numBytes = (uint)writer.BaseStream.Position;
            writer.BaseStream.Position = 0;

            writer.Write(data.numBytes);
            writer.Write(data.pointQuadtree);
            writer.Write(data.pointTriangle);
            writer.Write(data.pointVertex);
            writer.Write(data.quadCenterX);
            writer.Write(data.quadCenterY);
            writer.Write(data.quadCenterZ);
            writer.Write(data.quadLength);
            writer.Write(data.basePower);
            writer.Write(data.numTriangles);
            writer.Write(data.numVertices);
            writer.Write(data.numQuadnodes);

            File.WriteAllBytes(fileName, ((MemoryStream)writer.BaseStream).ToArray());
            return true;
        }

        public void ConvertCLtoOBJ(string fileName, ref CLFile data)
        {
            StreamWriter FileCloser = new StreamWriter(new FileStream(fileName, FileMode.Create));

            FileCloser.WriteLine("#Exported by Heroes Power Plant");
            FileCloser.WriteLine("#Number of vertices: " + data.CLVertexArray.Count().ToString());
            FileCloser.WriteLine("#Number of faces: " + data.CLTriangleArray.Count().ToString());
            FileCloser.WriteLine();

            foreach (VertexColoredNormalized i in data.CLVertexArray)
                FileCloser.WriteLine("v " + i.Position.X.ToString() + " " + i.Position.Y.ToString() + " " + i.Position.Z.ToString());

            FileCloser.WriteLine();

            foreach (UInt64 i in data.MeshTypeList)
            {
                byte[] TempByte = BitConverter.GetBytes(i);

                FileCloser.WriteLine("g mesh" +
                    TempByte[4].ToString("X2") + TempByte[5].ToString("X2") + "_" +
                    TempByte[0].ToString("X2") + TempByte[1].ToString("X2") +
                    TempByte[2].ToString("X2") + TempByte[3].ToString("X2"));
                FileCloser.WriteLine();

                foreach (Triangle j in data.CLTriangleArray)
                    if ((j.ColFlags[0] == TempByte[0]) & (j.ColFlags[1] == TempByte[1]) &
                        (j.ColFlags[2] == TempByte[2]) & (j.ColFlags[3] == TempByte[3]) &
                        (j.ColFlags[4] == TempByte[4]) & (j.ColFlags[5] == TempByte[5]))
                        FileCloser.WriteLine("f " + (j.Vertices[0] + 1).ToString() + " " + (j.Vertices[1] + 1).ToString() + " " + (j.Vertices[2] + 1).ToString());

                FileCloser.WriteLine();
            }

            FileCloser.Close();
        }

        public void GetClickedModelPosition(Ray ray, out bool hasIntersected, out float smallestDistance)
        {
            hasIntersected = false;
            smallestDistance = 40000f;

            foreach (Triangle t in data.CLTriangleArray)
            {
                Vector3 v1 = (Vector3)data.CLVertexArray[t.Vertices[0]].Position;
                Vector3 v2 = (Vector3)data.CLVertexArray[t.Vertices[1]].Position;
                Vector3 v3 = (Vector3)data.CLVertexArray[t.Vertices[2]].Position;

                if (ray.Intersects(ref v1, ref v2, ref v3, out float distance))
                {
                    hasIntersected = true;

                    if (distance < smallestDistance)
                        smallestDistance = distance;
                }
            }
        }
    }
}
