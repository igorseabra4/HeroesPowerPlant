using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null)
                return BoundingBox.FromPoints(cubeVertices.ToArray());
            else if (modelNames.Length == 0 | (byte)Type >= modelNames.Length | !DFFRenderer.DFFStream.ContainsKey(modelNames[(byte)Type]))
                return BoundingBox.FromPoints(cubeVertices.ToArray());

            return BoundingBox.FromPoints(DFFRenderer.DFFStream[modelNames[(byte)Type]].GetVertexList().ToArray());
        }
        
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if ((byte)Type < modelNames.Length)
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
            set { Write(4, (byte)value); }
        }

        public float MovingDistance
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}
