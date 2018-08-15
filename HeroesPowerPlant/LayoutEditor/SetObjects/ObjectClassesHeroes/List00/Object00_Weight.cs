using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum WeightType
    {
        Repeat = 0,
        Shadow = 1,
        Laser = 2,
        RepeatSwitch = 3,
        ShadowSwitch = 4,
        LaserSwitch = 5
    }

    public class Object00_Weight : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.Scaling(ScaleX + 1f, ScaleY + 1f, ScaleZ + 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public WeightType Type
        {
            get { return (WeightType)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public byte LinkID
        {
            get { return ReadByte(5); }
            set { Write(5, value); }
        }

        public short Height
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public float ScaleX
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ScaleY
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public short UpWaitTime
        {
            get { return ReadShort(16); }
            set { Write(16, value); }
        }

        public short DownWaitTime
        {
            get { return ReadShort(18); }
            set { Write(18, value); }
        }
    }
}