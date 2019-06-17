using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0587_GiantCasinoChip : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            base.CreateTransformMatrix(Position, Rotation);

            transformMatrix = Matrix.Scaling(Scale + 1f) * transformMatrix;
        }
        
        public float Scale
        {
            get => ReadFloat(4);
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public int Type
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public int Speed
        {
            get => ReadInt(12);
            set => Write(12, value);
        }
    }
}