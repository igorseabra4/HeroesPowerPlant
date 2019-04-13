using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_Spring : SetObjectManagerHeroes
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

        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public short NoControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }

        public float GuideLine
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
