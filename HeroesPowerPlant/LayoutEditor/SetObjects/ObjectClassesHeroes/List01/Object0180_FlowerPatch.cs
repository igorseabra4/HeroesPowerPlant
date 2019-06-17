using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0180_FlowerPatch : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            base.CreateTransformMatrix(Position, Rotation);

            transformMatrix = Matrix.Scaling(Scale) * transformMatrix;
        }

        public byte Type
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}
