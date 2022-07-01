using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionVertex
    {
        public Vector3 Position;
        public List<Vector3> NormalList;
        public Color Color;

        public CollisionVertex(float x, float y, float z)
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
}
