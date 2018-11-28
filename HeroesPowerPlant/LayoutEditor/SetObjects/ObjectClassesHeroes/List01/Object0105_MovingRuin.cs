using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null)
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());
            else if (modelNames.Length == 0 | (byte)Type >= modelNames.Length | !DFFRenderer.DFFModels.ContainsKey(modelNames[(byte)Type]))
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());

            return BoundingBox.FromPoints(DFFRenderer.DFFModels[modelNames[(byte)Type]].vertexListG.ToArray());
        }
        
        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if ((byte)Type < modelNames.Length)
                Draw(renderer, modelNames[(byte)Type], isSelected);
            else
                DrawCube(renderer, isSelected);
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
