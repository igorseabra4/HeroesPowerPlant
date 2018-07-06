using SharpDX;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static HeroesPowerPlant.SharpRenderer;

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
                    break;
                }
            }
        }
        
        public Vector3 Position;
        public byte Link;
        public byte Rend;

        public bool IsSelected;
        
        public override string ToString()
        {
            if (Link == 0)
                return objectEntry.GetName();
            else
                return objectEntry.GetName() + " (" + Link.ToString() + ")";
        }

        public abstract void CreateTransformMatrix();

        public abstract void Draw(bool drawEveryObject);

        public abstract void FindNewObjectManager();

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
                return distance;
            else return null;
        }
    }
}