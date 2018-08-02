using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionSystem
    {
        public string CurrentCLfileName;
        public int NumVertices { get => data.numVertices; }
        public int NumTriangles { get => data.numTriangles; }
        public int NumQuadNodes { get => data.numQuadnodes; }
        public byte DepthLevel { get => data.MaxDepth; }

        private CLFile data;

        public void Import(string sourceOBJfile, byte depthLevel)
        {
            ConvertOBJtoCL(sourceOBJfile, CurrentCLfileName, depthLevel);
        }

        public void NewFile(string sourceOBJfile, string destinationCLfile, byte depthLevel)
        {
            CurrentCLfileName = destinationCLfile;
            Import(sourceOBJfile, depthLevel);
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
            CollisionRendering.Dispose();
        }

        public bool HasOpenedFile()
        {
            return CurrentCLfileName != null;
        }

        public void LoadCLFile()
        {
            CollisionRendering.Dispose();

            data = CollisionRendering.LoadCLFile(CurrentCLfileName);
        }

        public void ConvertOBJtoCL(string InputFile, string OutputFile, byte depthLevel)
        {
            CLFile data = new CLFile(depthLevel);

            Program.collisionEditor.progressBar1.Minimum = 0;
            Program.collisionEditor.progressBar1.Value = 0;
            Program.collisionEditor.progressBar1.Step = 1;

            if (ReadOBJFile(InputFile, ref data))
                if (GenerateCollision(ref data))
                    CreateCLFile(OutputFile, ref data);
        }

        public bool ReadOBJFile(string InputFile, ref CLFile data)
        {
            string[] OBJFile = File.ReadAllLines(InputFile);
            Program.collisionEditor.progressBar1.Maximum = 65535 + OBJFile.Length;

            int CurrentMeshNum = -1;
            byte[] TempColFlags = { 0, 0, 0, 0x0 };

            List<CLTriangle> CLTriangleList = new List<CLTriangle>(65535);
            List<CLVertex> CLVertexList = new List<CLVertex>(65535);

            foreach (string j in OBJFile)
            {
                if (j.StartsWith("v "))
                {
                    string a = Regex.Replace(j, @"\s+", " ");
                    string[] SubStrings = a.Split(' ');
                    CLVertexList.Add(new CLVertex(Convert.ToSingle(SubStrings[1]), Convert.ToSingle(SubStrings[2]), Convert.ToSingle(SubStrings[3])));
                    Program.collisionEditor.progressBar1.PerformStep();
                }
                else if (j.StartsWith("f "))
                {
                    string[] SubStrings = j.Split(' ');
                    CLTriangleList.Add(new CLTriangle(
                    (ushort)(Convert.ToUInt16(SubStrings[1].Split('/')[0]) - 1),
                    (ushort)(Convert.ToUInt16(SubStrings[2].Split('/')[0]) - 1),
                    (ushort)(Convert.ToUInt16(SubStrings[3].Split('/')[0]) - 1),
                    CurrentMeshNum, TempColFlags, CLVertexList));
                    Program.collisionEditor.progressBar1.PerformStep();
                }
                else if (j.StartsWith("g ") | j.StartsWith("o "))
                {
                    CurrentMeshNum += 1;
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

            Program.collisionEditor.progressBar1.Maximum = 65535 + CLVertexList.Count() + CLTriangleList.Count();

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

        public bool GenerateCollision(ref CLFile data)
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

            data.quadLenght = MaxX - MinX;
            if (data.quadLenght < MaxZ - MinZ)
                data.quadLenght = MaxZ - MinZ;

            data.quadLenght = (float)Math.Ceiling(data.quadLenght);
            while (data.quadLenght % 16 != 0)
                data.quadLenght++;

            if (data.MaxDepth == 0)
            {
                data.MaxDepth = (byte)(Math.Log(data.quadLenght / 50) / (Math.Log(2)));
                if (data.MaxDepth > 10)
                    data.MaxDepth = 10;
            }

            data.numTriangles = (ushort)data.CLTriangleArray.Count();
            data.numVertices = (ushort)data.CLVertexArray.Count();

            //Now let's build the quadtree
            data.PowerFlag = (ushort)Program.collisionEditor.numericUpDownPowerFlag.Value;

            if (BuildQuadtree(ref data))
            {
                data.numQuadnodes = (ushort)data.CLQuadNodeList.Count;
                return true;
            }

            return false;
        }

        public bool BuildQuadtree(ref CLFile data)
        {
            CLQuadNode TempNode = new CLQuadNode();

            TempNode.NodeSquare.X = data.quadCenterX - (data.quadLenght / 2);
            TempNode.NodeSquare.Y = data.quadCenterZ - (data.quadLenght / 2);
            TempNode.NodeSquare.Width = data.quadLenght;
            TempNode.NodeSquare.Height = data.quadLenght;

            TempNode.Child = 1;
            TempNode.NodeTriangleArray = Range((ushort)data.CLTriangleArray.Count());

            data.CLQuadNodeList.Add(TempNode);
            Program.collisionEditor.progressBar1.PerformStep();

            int i = 0;

            while (i < data.CLQuadNodeList.Count)
            {
                if (data.CLQuadNodeList[i].Depth != data.MaxDepth & data.CLQuadNodeList[i].NodeTriangleArray.Count() > 0)
                {
                    data.CLQuadNodeList[i].Child = (ushort)data.CLQuadNodeList.Count;

                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 0, (ushort)data.CLQuadNodeList.Count, data.PowerFlag, data.CLTriangleArray));
                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 1, (ushort)data.CLQuadNodeList.Count, data.PowerFlag, data.CLTriangleArray));
                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 2, (ushort)data.CLQuadNodeList.Count, data.PowerFlag, data.CLTriangleArray));
                    data.CLQuadNodeList.Add(CreateNode(data.CLQuadNodeList[i], 3, (ushort)data.CLQuadNodeList.Count, data.PowerFlag, data.CLTriangleArray));

                    data.CLQuadNodeList[i].NodeTriangleArray = null;
                    data.CLQuadNodeList[i].NodeTriangleAmount = 0;
                }
                i += 1;
            }

            Program.collisionEditor.progressBar1.Maximum = 2 * data.CLVertexArray.Count() + 2 * data.CLTriangleArray.Count() + 4 * data.CLQuadNodeList.Count();
            return true;
        }

        public CLQuadNode CreateNode(CLQuadNode NodeParent, byte NodeOrient, ushort Count, int PowerFlag, CLTriangle[] CLTriangleArray)
        {
            CLQuadNode NodeChild = new CLQuadNode
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

            Program.collisionEditor.progressBar1.PerformStep();
            return NodeChild;
        }

        public UInt16[] GetTrianglesInsideNode(CLQuadNode Node, ushort[] TriangleList, CLTriangle[] CLTriangleArray)
        {
            List<UInt16> NodeTriangleList = new List<UInt16>();

            foreach (ushort i in TriangleList)
                if (Node.NodeSquare.Intersects(CLTriangleArray[i].TasRect))
                    NodeTriangleList.Add(i);

            return NodeTriangleList.ToArray();
        }

        public bool CreateCLFile(string FileName, ref CLFile data)
        {
            //Finally, let's write the file
            BinaryWriter FileWriter = new BinaryWriter(new MemoryStream(
                0x20 * data.CLQuadNodeList.Count() +
                0x20 * data.CLTriangleArray.Count() +
                0xC * data.CLVertexArray.Count() + 0x28));

            FileWriter.BaseStream.Position = 0x28;
            for (ushort i = 0; i < data.CLQuadNodeList.Count; i++)
            {
                if ((data.CLQuadNodeList[i].Depth == data.MaxDepth) & (data.CLQuadNodeList[i].NodeTriangleAmount > 0))
                {
                    data.CLQuadNodeList[i].TriListOffset = (uint)FileWriter.BaseStream.Position;
                    foreach (UInt16 j in data.CLQuadNodeList[i].NodeTriangleArray)
                    {
                        FileWriter.Write(Switch(j));
                    }
                }
                Program.collisionEditor.progressBar1.PerformStep();
            }

            if (FileWriter.BaseStream.Position % 4 == 2)
                FileWriter.Write((ushort)0);

            data.pointQuadtree = (uint)FileWriter.BaseStream.Position;

            foreach (CLQuadNode i in data.CLQuadNodeList)
            {
                FileWriter.Write(Switch(i.Index));
                FileWriter.Write(Switch(i.Parent));
                FileWriter.Write(Switch(i.Child));
                FileWriter.Write((ushort)0);
                FileWriter.Write((ushort)0);
                FileWriter.Write((ushort)0);
                FileWriter.Write((ushort)0);
                FileWriter.Write(Switch(i.NodeTriangleAmount));
                FileWriter.Write(Switch(i.TriListOffset));
                FileWriter.Write(Switch(i.PosValueX));
                FileWriter.Write(Switch(i.PosValueZ));
                FileWriter.Write(i.Depth);
                FileWriter.Write((byte)0);
                FileWriter.Write((ushort)0);
                FileWriter.Write(0);

                Program.collisionEditor.progressBar1.PerformStep();
            }

            data.pointTriangle = (uint)FileWriter.BaseStream.Position;

            foreach (CLTriangle i in data.CLTriangleArray)
            {
                FileWriter.Write(Switch(i.Vertices[0]));
                FileWriter.Write(Switch(i.Vertices[1]));
                FileWriter.Write(Switch(i.Vertices[2]));
                FileWriter.Write((ushort)0xFFFF);
                FileWriter.Write((ushort)0xFFFF);
                FileWriter.Write((ushort)0xFFFF);
                FileWriter.Write(Switch(i.Normals.X));
                FileWriter.Write(Switch(i.Normals.Y));
                FileWriter.Write(Switch(i.Normals.Z));
                FileWriter.Write(i.ColFlags[0]);
                FileWriter.Write(i.ColFlags[1]);
                FileWriter.Write(i.ColFlags[2]);
                FileWriter.Write(i.ColFlags[3]);
                FileWriter.Write(Switch(i.MeshNum));
                FileWriter.Write((ushort)0);

                Program.collisionEditor.progressBar1.PerformStep();
            }

            data.pointVertex = (uint)FileWriter.BaseStream.Position;

            foreach (VertexColoredNormalized i in data.CLVertexArray)
            {
                FileWriter.Write(Switch(i.Position.X));
                FileWriter.Write(Switch(i.Position.Y));
                FileWriter.Write(Switch(i.Position.Z));

                Program.collisionEditor.progressBar1.PerformStep();
            }

            data.numBytes = (uint)FileWriter.BaseStream.Position;
            FileWriter.BaseStream.Position = 0;

            FileWriter.Write(Switch(data.numBytes));
            FileWriter.Write(Switch(data.pointQuadtree));
            FileWriter.Write(Switch(data.pointTriangle));
            FileWriter.Write(Switch(data.pointVertex));
            FileWriter.Write(Switch(data.quadCenterX));
            FileWriter.Write(Switch(data.quadCenterY));
            FileWriter.Write(Switch(data.quadCenterZ));
            FileWriter.Write(Switch(data.quadLenght));
            FileWriter.Write(Switch(data.PowerFlag));
            FileWriter.Write(Switch(data.numTriangles));
            FileWriter.Write(Switch(data.numVertices));
            FileWriter.Write(Switch(data.numQuadnodes));

            File.WriteAllBytes(FileName, ((MemoryStream)FileWriter.BaseStream).ToArray());
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

                foreach (CLTriangle j in data.CLTriangleArray)
                    if ((j.ColFlags[0] == TempByte[0]) & (j.ColFlags[1] == TempByte[1]) &
                        (j.ColFlags[2] == TempByte[2]) & (j.ColFlags[3] == TempByte[3]) &
                        (j.ColFlags[4] == TempByte[4]) & (j.ColFlags[5] == TempByte[5]))
                        FileCloser.WriteLine("f " + (j.Vertices[0] + 1).ToString() + " " + (j.Vertices[1] + 1).ToString() + " " + (j.Vertices[2] + 1).ToString());

                FileCloser.WriteLine();
            }

            FileCloser.Close();
        }
    }
}
