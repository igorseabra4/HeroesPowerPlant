using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum GhostType
    {
        NoMove = 0,
        Line = 1,
        Circle = 2
    }

    public class Object1105_Ghost : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.Scaling(Scale + 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public GhostType Type
        {
            get => (GhostType)ReadLong(4);
            set => Write(4, (int)value);
        }

        public float Range
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MovingArea
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Speed
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Scale
        {
            get => ReadFloat(20);
            set { Write(20, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}
