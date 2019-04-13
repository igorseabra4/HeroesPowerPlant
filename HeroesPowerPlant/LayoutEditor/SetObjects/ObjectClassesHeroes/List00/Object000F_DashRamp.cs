using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y) + MathUtil.Pi) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
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

        public short ControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }
    }
}