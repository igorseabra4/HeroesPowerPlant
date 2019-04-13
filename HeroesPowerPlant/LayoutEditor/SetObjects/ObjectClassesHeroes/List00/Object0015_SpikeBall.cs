using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_SpikeBall : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale + 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public enum SpikeBallType
        {
            SingleBall = 0,
            DoubleBall = 1
        }

        public SpikeBallType Type
        {
            get => (SpikeBallType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float RotateSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Scale
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}