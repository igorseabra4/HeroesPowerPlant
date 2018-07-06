using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.Collision
{
    public partial class CollisionFunctions
    {
        public struct Header
        {
            public UInt32 numBytes;
            public UInt32 pointQuadtree;
            public UInt32 pointTriangle;
            public UInt32 pointVertex;
            public float quadCenterX;
            public float quadCenterY;
            public float quadCenterZ;
            public float quadLenght;
            public UInt16 PowerFlag;
            public UInt16 numTriangles;
            public UInt16 numVertices;
            public UInt16 numQuadnodes;
        }

        public class CLQuadNode
        {
            public UInt16 Index;
            public UInt16 Parent;
            public UInt16 Child;
            public UInt16 NodeTriangleAmount;
            public UInt32 TriListOffset;
            public UInt16 PosValueX;
            public UInt16 PosValueZ;

            public byte Depth;
            public UInt16[] NodeTriangleArray;

            public RectangleF NodeSquare;

            public override string ToString()
            {
                return "Nd. " + Index.ToString() + ", Pr. " + Parent.ToString() + ", Ch. " + Child.ToString() + ", Am. " + NodeTriangleAmount.ToString() + ", D. " + Depth.ToString();
            }
        }

        public class CLVertex
        {
            public Vector3 Position;
            public List<Vector3> NormalList;
            public Color Color;

            public CLVertex(float x, float y, float z)
            {
                Position = new Vector3(x, y, z);
                NormalList = new List<Vector3>(3);
                Color = Color.White;
            }

            public Vector3 CalculateNormals()
            {
                Vector3 Totals = new Vector3();
                foreach (Vector3 j in NormalList)
                    Totals += j;
                Totals.Normalize();

                return Totals;
            }
        }

        public class CLTriangle
        {
            public ushort[] Vertices = new ushort[3];
            public Vector3 Normals;
            public byte[] ColFlags = new byte[4];
            public UInt16 MeshNum;

            public RectangleF TasRect;

            public CLTriangle(UInt16 a, UInt16 b, UInt16 c, int d, byte[] e)
            {
                Vertices[0] = a;

                if (Program.collisionEditor.checkBox2.CheckState == CheckState.Checked)
                {
                    Vertices[1] = c;
                    Vertices[2] = b;
                }
                else
                {
                    Vertices[1] = b;
                    Vertices[2] = c;
                }

                MeshNum = (ushort)d;
                ColFlags = e;

                CalculateNormals();
                CalculateRectangle();
            }

            public CLTriangle(uint a, uint b, uint c)
            {
                Vertices[0] = (ushort)a;
                Vertices[1] = (ushort)b;
                Vertices[2] = (ushort)c;
            }

            public void CalculateNormals()
            {
                Vector3 Vector1 = new Vector3(
                    CLVertexList[Vertices[1]].Position.X - CLVertexList[Vertices[0]].Position.X,
                    CLVertexList[Vertices[1]].Position.Y - CLVertexList[Vertices[0]].Position.Y,
                    CLVertexList[Vertices[1]].Position.Z - CLVertexList[Vertices[0]].Position.Z);
                Vector3 Vector2 = new Vector3(
                    CLVertexList[Vertices[2]].Position.X - CLVertexList[Vertices[0]].Position.X,
                    CLVertexList[Vertices[2]].Position.Y - CLVertexList[Vertices[0]].Position.Y,
                    CLVertexList[Vertices[2]].Position.Z - CLVertexList[Vertices[0]].Position.Z);

                Normals = Vector3.Cross(Vector1, Vector2);

                Normals.Normalize();

                CLVertexList[Vertices[0]].NormalList.Add(Normals);
                CLVertexList[Vertices[1]].NormalList.Add(Normals);
                CLVertexList[Vertices[2]].NormalList.Add(Normals);
            }

            public void CalculateRectangle()
            {
                float MinX = CLVertexList[Vertices[0]].Position.X;
                if (CLVertexList[Vertices[1]].Position.X < MinX)
                    MinX = CLVertexList[Vertices[1]].Position.X;
                if (CLVertexList[Vertices[2]].Position.X < MinX)
                    MinX = CLVertexList[Vertices[2]].Position.X;

                float MinZ = CLVertexList[Vertices[0]].Position.Z;
                if (CLVertexList[Vertices[1]].Position.Z < MinZ)
                    MinZ = CLVertexList[Vertices[1]].Position.Z;
                if (CLVertexList[Vertices[2]].Position.Z < MinZ)
                    MinZ = CLVertexList[Vertices[2]].Position.Z;

                float MaxX = CLVertexList[Vertices[0]].Position.X;
                if (CLVertexList[Vertices[1]].Position.X > MaxX)
                    MaxX = CLVertexList[Vertices[1]].Position.X;
                if (CLVertexList[Vertices[2]].Position.X > MaxX)
                    MaxX = CLVertexList[Vertices[2]].Position.X;

                float MaxZ = CLVertexList[Vertices[0]].Position.Z;
                if (CLVertexList[Vertices[1]].Position.Z > MaxZ)
                    MaxZ = CLVertexList[Vertices[1]].Position.Z;
                if (CLVertexList[Vertices[2]].Position.Z > MaxZ)
                    MaxZ = CLVertexList[Vertices[2]].Position.Z;

                TasRect = new RectangleF(MinX, MinZ, MaxX - MinX, MaxZ - MinZ);
            }
        }

        public static Header UseHeader = new Header();
        public static List<CLQuadNode> CLQuadNodeList = new List<CLQuadNode>();
        public static CLTriangle[] CLTriangleArray;
        public static List<CLVertex> CLVertexList = new List<CLVertex>();
        public static VertexColoredNormalized[] CLVertexArray;

        public static void ConvertOBJtoCL(string InputFile, string OutputFile, byte depthLevel)
        {
            UseHeader = new Header();
            CLQuadNodeList.Clear();
            CLVertexArray = null;
            CLTriangleArray = null;

            Program.collisionEditor.progressBar1.Minimum = 0;
            Program.collisionEditor.progressBar1.Value = 0;
            Program.collisionEditor.progressBar1.Step = 1;

            if (ReadOBJFile(InputFile))
                if (GenerateCollision(depthLevel))
                    CreateCLFile(OutputFile, depthLevel);
        }

        public static bool ReadOBJFile(string InputFile)
        {
            string[] OBJFile = File.ReadAllLines(InputFile);
            Program.collisionEditor.progressBar1.Maximum = 65535 + OBJFile.Length;

            int CurrentMeshNum = -1;
            byte[] TempColFlags = { 0, 0, 0, 0x0 };

            List<CLTriangle> CLTriangleList = new List<CLTriangle>(65535);
            CLVertexList = new List<CLVertex>(65535);

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
                    CurrentMeshNum, TempColFlags));
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

            CLTriangleArray = CLTriangleList.ToArray();
            return true;
        }

        public static bool GenerateCollision(byte MaxDepth)
        {
            //Let's start with quadtree maximums, minimums and center
            float MaxX = CLVertexList[0].Position.X;
            float MaxY = CLVertexList[0].Position.Y;
            float MaxZ = CLVertexList[0].Position.Z;
            float MinX = CLVertexList[0].Position.X;
            float MinY = CLVertexList[0].Position.Y;
            float MinZ = CLVertexList[0].Position.Z;

            foreach (CLVertex i in CLVertexList)
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

            UseHeader.quadCenterX = (MaxX + MinX) / 2.0f;
            UseHeader.quadCenterY = (MaxY + MinY) / 2.0f;
            UseHeader.quadCenterZ = (MaxZ + MinZ) / 2.0f;

            UseHeader.quadLenght = MaxX - MinX;
            if (UseHeader.quadLenght < MaxZ - MinZ)
                UseHeader.quadLenght = MaxZ - MinZ;

            UseHeader.quadLenght = (float)Math.Ceiling(UseHeader.quadLenght);
            while (UseHeader.quadLenght % 16 != 0)
                UseHeader.quadLenght++;

            if (Program.collisionEditor.checkBox1.Checked == true)
            {
                MaxDepth = (byte)(Math.Log(UseHeader.quadLenght / 50) / (Math.Log(2)));
                if (MaxDepth > 10)
                    MaxDepth = 10;
            }

            UseHeader.numTriangles = (ushort)CLTriangleArray.Count();
            UseHeader.numVertices = (ushort)CLVertexList.Count();

            //Now let's build the quadtree
            Program.collisionEditor.numericDepthLevel.Value = MaxDepth;
            UseHeader.PowerFlag = (ushort)Program.collisionEditor.numericUpDownPowerFlag.Value;

            if (BuildQuadtree(MaxDepth))
            {
                UseHeader.numQuadnodes = (ushort)CLQuadNodeList.Count;
                return true;
            }
            else
                return false;
        }

        public static bool BuildQuadtree(byte MaxDepth)
        {
            CLQuadNode TempNode = new CLQuadNode();

            TempNode.NodeSquare.X = UseHeader.quadCenterX - (UseHeader.quadLenght / 2);
            TempNode.NodeSquare.Y = UseHeader.quadCenterZ - (UseHeader.quadLenght / 2);
            TempNode.NodeSquare.Width = UseHeader.quadLenght;
            TempNode.NodeSquare.Height = UseHeader.quadLenght;

            TempNode.Child = 1;
            TempNode.NodeTriangleArray = ReadWriteCommon.Range(CLTriangleArray.Count()).Cast<ushort>().ToArray();

            CLQuadNodeList.Add(TempNode);
            Program.collisionEditor.progressBar1.PerformStep();

            int i = 0;

            while (i < CLQuadNodeList.Count)
            {
                if (CLQuadNodeList[i].Depth != MaxDepth & CLQuadNodeList[i].NodeTriangleArray.Count() > 0)
                {
                    CLQuadNodeList[i].Child = (ushort)CLQuadNodeList.Count;

                    CLQuadNodeList.Add(CreateNode(CLQuadNodeList[i], 0));
                    CLQuadNodeList.Add(CreateNode(CLQuadNodeList[i], 1));
                    CLQuadNodeList.Add(CreateNode(CLQuadNodeList[i], 2));
                    CLQuadNodeList.Add(CreateNode(CLQuadNodeList[i], 3));

                    CLQuadNodeList[i].NodeTriangleArray = null;
                    CLQuadNodeList[i].NodeTriangleAmount = 0;
                }
                i += 1;
            }

            Program.collisionEditor.progressBar1.Maximum = 2 * CLVertexList.Count() + 2 * CLTriangleArray.Count() + 4 * CLQuadNodeList.Count();
            return true;
        }

        public static CLQuadNode CreateNode(CLQuadNode NodeParent, byte NodeOrient)
        {
            CLQuadNode NodeChild = new CLQuadNode
            {
                Index = (ushort)CLQuadNodeList.Count,
                Parent = NodeParent.Index,
                Child = 0,
                Depth = (byte)(NodeParent.Depth + 1),

                PosValueX = NodeParent.PosValueX,
                PosValueZ = NodeParent.PosValueZ,

                NodeSquare = new RectangleF(NodeParent.NodeSquare.X, NodeParent.NodeSquare.Y, NodeParent.NodeSquare.Width / 2f, NodeParent.NodeSquare.Height / 2f)
            };

            ushort ValueToAdd = (ushort)Math.Pow(2, (UseHeader.PowerFlag - NodeChild.Depth));

            if (NodeOrient == 1)
            {
                NodeChild.PosValueX += ValueToAdd;
                NodeChild.NodeSquare.X += NodeChild.NodeSquare.Width;
            }
            else if (NodeOrient == 2)
            {
                NodeChild.PosValueZ += ValueToAdd;
                NodeChild.NodeSquare.Y += NodeChild.NodeSquare.Height;
            }
            else if (NodeOrient == 3)
            {
                NodeChild.PosValueX += ValueToAdd;
                NodeChild.NodeSquare.X += NodeChild.NodeSquare.Width;
                NodeChild.PosValueZ += ValueToAdd;
                NodeChild.NodeSquare.Y += NodeChild.NodeSquare.Height;
            }

            NodeChild.NodeTriangleArray = GetTrianglesInsideNode(NodeChild, NodeParent.NodeTriangleArray);
            NodeChild.NodeTriangleAmount = (ushort)NodeChild.NodeTriangleArray.Count();

            Program.collisionEditor.progressBar1.PerformStep();
            return NodeChild;
        }

        public static UInt16[] GetTrianglesInsideNode(CLQuadNode Node, ushort[] TriangleList)
        {
            List<UInt16> NodeTriangleList = new List<UInt16>();

            foreach (ushort i in TriangleList)
                if (Node.NodeSquare.Intersects(CLTriangleArray[i].TasRect))
                    NodeTriangleList.Add(i);

            return NodeTriangleList.ToArray();
        }

        public static bool CreateCLFile(string FileName, byte MaxDepth)
        {
            //Finally, let's write the file
            BinaryWriter FileWriter = new BinaryWriter(new MemoryStream(
                0x20 * CLQuadNodeList.Count() +
                0x20 * CLTriangleArray.Count() +
                0xC * CLVertexArray.Count() + 0x28));
            
            FileWriter.BaseStream.Position = 0x28;
            for (ushort i = 0; i < CLQuadNodeList.Count; i++)
            {
                if ((CLQuadNodeList[i].Depth == MaxDepth) & (CLQuadNodeList[i].NodeTriangleAmount > 0))
                {
                    CLQuadNodeList[i].TriListOffset = (uint)FileWriter.BaseStream.Position;
                    foreach (UInt16 j in CLQuadNodeList[i].NodeTriangleArray)
                    {
                        FileWriter.Write(Switch(j));
                    }
                }
                Program.collisionEditor.progressBar1.PerformStep();
            }

            if (FileWriter.BaseStream.Position % 4 == 2)
                FileWriter.Write((ushort)0);

            UseHeader.pointQuadtree = (uint)FileWriter.BaseStream.Position;

            foreach (CLQuadNode i in CLQuadNodeList)
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

            UseHeader.pointTriangle = (uint)FileWriter.BaseStream.Position;

            foreach (CLTriangle i in CLTriangleArray)
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

            UseHeader.pointVertex = (uint)FileWriter.BaseStream.Position;

            foreach (CLVertex i in CLVertexList)
            {
                FileWriter.Write(Switch(i.Position.X));
                FileWriter.Write(Switch(i.Position.Y));
                FileWriter.Write(Switch(i.Position.Z));

                Program.collisionEditor.progressBar1.PerformStep();
            }
            
            UseHeader.numBytes = (uint)FileWriter.BaseStream.Position;
            FileWriter.BaseStream.Position = 0;

            FileWriter.Write(Switch(UseHeader.numBytes));
            FileWriter.Write(Switch(UseHeader.pointQuadtree));
            FileWriter.Write(Switch(UseHeader.pointTriangle));
            FileWriter.Write(Switch(UseHeader.pointVertex));
            FileWriter.Write(Switch(UseHeader.quadCenterX));
            FileWriter.Write(Switch(UseHeader.quadCenterY));
            FileWriter.Write(Switch(UseHeader.quadCenterZ));
            FileWriter.Write(Switch(UseHeader.quadLenght));
            FileWriter.Write(Switch(UseHeader.PowerFlag));
            FileWriter.Write(Switch(UseHeader.numTriangles));
            FileWriter.Write(Switch(UseHeader.numVertices));
            FileWriter.Write(Switch(UseHeader.numQuadnodes));
            
            File.WriteAllBytes(FileName, ((MemoryStream)FileWriter.BaseStream).ToArray());
            return true;
        }

        static List<UInt64> MeshTypeList;

        public static void ConvertCLtoOBJ(string fileName)
        {
            StreamWriter FileCloser = new StreamWriter(new FileStream(fileName, FileMode.Create));

            FileCloser.WriteLine("#Exported by Heroes Power Plant");
            FileCloser.WriteLine("#Number of vertices: " + CLVertexArray.Count().ToString());
            FileCloser.WriteLine("#Number of faces: " + CLTriangleArray.Count().ToString());
            FileCloser.WriteLine();

            foreach (VertexColoredNormalized i in CLVertexArray)
                FileCloser.WriteLine("v " + i.Position.X.ToString() + " " + i.Position.Y.ToString() + " " + i.Position.Z.ToString());

            FileCloser.WriteLine();

            foreach (UInt64 i in MeshTypeList)
            {
                byte[] TempByte = BitConverter.GetBytes(i);

                FileCloser.WriteLine("g mesh" +
                    TempByte[4].ToString("X2") + TempByte[5].ToString("X2") + "_" +
                    TempByte[0].ToString("X2") + TempByte[1].ToString("X2") +
                    TempByte[2].ToString("X2") + TempByte[3].ToString("X2"));
                FileCloser.WriteLine();

                foreach (CLTriangle j in CLTriangleArray)
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
