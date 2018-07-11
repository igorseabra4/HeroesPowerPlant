using System;
using System.Collections.Generic;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null | modelNames.Length == 0)
                return BoundingBox.FromPoints(cubeVertices.ToArray());

            List<Vector3> list = new List<Vector3>();

            if (MiscSettings[4] < 3)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[(byte)Type]))
                    list.AddRange(DFFRenderer.DFFStream[modelNames[(byte)Type]].GetVertexList());
                else
                    list.AddRange(cubeVertices);
            else
                list.AddRange(cubeVertices);
            
            return BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (MiscSettings[4] < 3)
                Draw(modelNames[(byte)Type], isSelected);
            else
                DrawCube(isSelected);
        }

        public enum RuinType : byte
        {
            Small = 0,
            Normal = 1,
            Special = 2
        }

        public RuinType Type
        {
            get { return (RuinType)ReadByte(4); }
            set
            {
                byte a = (byte)value;
                Write(4, a);
            }
        }

        public float MovingDistance
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}
