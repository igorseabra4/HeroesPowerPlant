using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectHeroes
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

        public float SpeedHorizontal
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float SpeedVertical
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public short NoControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }
    }
}