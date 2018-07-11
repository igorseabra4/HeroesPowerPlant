using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0032_WarpFlower : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale != 0f ? Scale : 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public enum FlowerType : byte
        {
            Item = 0,
            Scaffold = 1,
            Warp = 2
        }

        public FlowerType Type
        {
            get { return (FlowerType)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public float Scale
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float RisingHeight
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}