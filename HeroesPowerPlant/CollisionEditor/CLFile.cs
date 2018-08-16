using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CLFile
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

        public List<QuadNode> CLQuadNodeList = new List<QuadNode>();
        public Triangle[] CLTriangleArray;
        public VertexColoredNormalized[] CLVertexArray;
        public byte MaxDepth;

        public CLFile(byte MaxDepth)
        {
            this.MaxDepth = MaxDepth;
        }

        public List<UInt64> MeshTypeList;
    }
}
