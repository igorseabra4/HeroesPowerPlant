using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum RainbowType : short
    {
        Speed = 0,
        FlyA = 1,
        FlyB = 2,
        PowerS = 3,
        PowerL = 4
    }

    public class Object000D_BigRings : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix =
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y) + MathUtil.Pi) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);

            CreateBoundingBox();
        }

        public RainbowType RainbowType
        {
            get => (RainbowType)ReadShort(4);
            set => Write(4, (short)value);
        }

        public short AdditionalControlTime
        {
            get => ReadShort(6);
            set => Write(6, value);
        }

        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Offset
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}