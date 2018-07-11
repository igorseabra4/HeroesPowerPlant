using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObject
    {
        public ObjectEntry objectEntry;
        public void FindObjectEntry(byte List, byte Type, ObjectEntry[] objectEntries)
        {
            for (int i = 0; i < objectEntries.Length; i++)
            {
                if (objectEntries[i].List == List & objectEntries[i].Type == Type)
                {
                    objectEntry = objectEntries[i];
                    return;
                }
            }
            throw new Exception($"Object entry not found: {List} {Type}");
        }
        
        public Vector3 Position;
        public Vector3 Rotation;
        public byte Link;
        public byte Rend;

        public bool isSelected;
        
        public override string ToString()
        {
            return objectEntry.GetName() + (Link == 0 ? "" : $"({Link})");
        }

        public abstract void Draw(bool drawEveryObject);

        public abstract void FindNewObjectManager();

        public abstract void CreateTransformMatrix();

        public int GetTypeAsOne()
        {
            return BitConverter.ToInt32(new byte[] { Link, objectEntry.Type, objectEntry.List, 0 }, 0);
        }

        public float GetDistanceFromOrigin()
        {
            return Position.Length();
        }

        public BoundingBox boundingBox;

        public float? IntersectsWith(Ray r)
        {
            if (r.Intersects(ref boundingBox, out float distance))
            {
                if (!TriangleIntersection(r))
                    return null;

                return distance;
            }
            else return null;
        }

        public abstract bool TriangleIntersection(Ray r);
    }
}