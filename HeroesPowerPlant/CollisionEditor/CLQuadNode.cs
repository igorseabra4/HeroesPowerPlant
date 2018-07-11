using System;
using SharpDX;

namespace HeroesPowerPlant.CollisionEditor
{
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
}
