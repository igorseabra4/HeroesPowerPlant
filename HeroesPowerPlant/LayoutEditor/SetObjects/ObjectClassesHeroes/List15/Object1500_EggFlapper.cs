using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum QualityType : byte
    {
        Normal = 0,
        Silver = 1
    }

    public enum ShadowType : byte
    {
        Auto = 0,
        Set = 1,
        SetWithoutShadow = 2
    }

    public enum MoveType : byte
    {
        Wait = 0,
        BackAndForth = 1,
        Wander = 2,
        Idle = 3
    }

    public enum WeaponType : byte
    {
        None = 0,
        Needle = 1,
        Shot = 2,
        MGun = 3,
        Laser = 4,
        Bomb = 5,
        Searchlight = 6
    }

    public class Object1500_EggFlapper : SetObjectManagerHeroes
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

        public QualityType Quality
        {
            get { return (QualityType)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public ShadowType ShadowType
        {
            get { return (ShadowType)ReadByte(5); }
            set { byte a = (byte)value; Write(5, a); }
        }

        public MoveType MoveType
        {
            get { return (MoveType)ReadByte(6); }
            set { byte a = (byte)value; Write(6, a); }
        }

        public WeaponType WeaponType
        {
            get { return (WeaponType)ReadByte(7); }
            set { byte a = (byte)value; Write(7, a); }
        }

        public short ScopeRange
        {
            //2.1
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public short ScopeOffset
        {
            //2.2
            get { return ReadShort(10); }
            set { Write(10, value); }
        }

        public short AttackInterval
        {
            //3.1
            get { return ReadShort(12); }
            set { Write(12, value); }
        }

        public short AttackFrame
        {
            //3.2
            get { return ReadShort(14); }
            set { Write(14, value); }
        }

        public float FLOOR
        {
            //4
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float MoveSpeed
        {
            //5
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public float MoveRange
        {
            //6
            get { return ReadFloat(24); }
            set { Write(24, value); }
        }

        public float WeaponSpeed
        {
            //7
            get { return ReadFloat(28); }
            set { Write(28, value); }
        }

        public short LightAngleY
        {
            //W8.1
            get { return ReadShort(32); }
            set { Write(32, value); }
        }

        public short LightAngleX
        {
            //W8.2
            get { return ReadShort(34); }
            set { Write(34, value); }
        }
    }
}
