using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1184_SmokeScreen : SetObjectManagerHeroes
    {        
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            base.CreateTransformMatrix(Position, Rotation);

            transformMatrix = IsUpsideDown ? Matrix.RotationY(MathUtil.Pi) : Matrix.Identity *
                transformMatrix;
        }

        public int ModelNumber
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public bool IsUpsideDown
        {
            get => ReadByte(12) != 0;
            set => Write(12, (byte)(value ? 1 : 0));
        }
    }
}
