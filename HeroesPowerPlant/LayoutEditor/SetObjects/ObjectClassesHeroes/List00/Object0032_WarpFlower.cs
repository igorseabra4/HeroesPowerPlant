using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum FlowerType : byte
    {
        Item = 0,
        Scaffold = 1,
        Warp = 2
    }

    public class Object0032_WarpFlower : SetObjectManagerHeroes
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

        public FlowerType Type
        {
            get => (FlowerType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(); }
        }

        public float RisingHeight
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}