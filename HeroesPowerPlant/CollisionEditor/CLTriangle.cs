using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpDX;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CLTriangle
    {
        public ushort[] Vertices = new ushort[3];
        public Vector3 Normals;
        public byte[] ColFlags = new byte[4];
        public UInt16 MeshNum;

        public RectangleF TasRect;

        public CLTriangle(UInt16 a, UInt16 b, UInt16 c, int d, byte[] e, List<CLVertex> CLVertexList)
        {
            Vertices[0] = a;

            if (Program.CollisionEditor.checkBox2.CheckState == CheckState.Checked)
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

            CalculateNormals(CLVertexList);
            CalculateRectangle(CLVertexList);
        }

        public CLTriangle(uint a, uint b, uint c)
        {
            Vertices[0] = (ushort)a;
            Vertices[1] = (ushort)b;
            Vertices[2] = (ushort)c;
        }

        public void CalculateNormals(List<CLVertex> CLVertexList)
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

        public void CalculateRectangle(List<CLVertex> CLVertexList)
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
}