using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1510_EggPawn : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        public enum StartModeEnum : byte
        {
            Asleep = 0,
            Wandering = 1,
            Running = 2,
            Fall = 3,
            Warp = 4,
            Falco = 5,
            Searching = 6
        }
        public StartModeEnum StartMode
        {
            get => (StartModeEnum)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public enum MassTypeEnum : byte
        {
            NormalFree = 0,
            NormalStand = 1,
            KingFree = 2,
            KingStand = 3,
            Casino1Free = 4,
            Casino1Stand = 5,
            Casino2Free = 6,
            Casino2Stand = 7
        }
        public MassTypeEnum ColorMass
        {
            get => (MassTypeEnum)ReadByte(5);
            set => Write(5, (byte)value);
        }

        public enum WeaponTypeEnum : byte
        {
            None = 0,
            Lance = 1,
            LaserCannon = 2,
            MGun90 = 3,
            MGun120 = 4,
            MGun150 = 5,
            MGun180 = 6
        }
        public WeaponTypeEnum WeaponType
        {
            get => (WeaponTypeEnum)ReadByte(6);
            set => Write(6, (byte)value);
        }

        public enum ShieldTypeEnum : byte
        {
            None = 0,
            Concrete = 1,
            Plain = 2,
            Spike = 3
        }
        public ShieldTypeEnum ShieldType
        {
            get => (ShieldTypeEnum)ReadByte(7);
            set => Write(7, (byte)value);
        }

        public short ScopeRange
        {
            //W2
            get => ReadShort(8);
            set => Write(8, value);
        }

        public short ScopeOffset
        {
            //W2
            get => ReadShort(10);
            set => Write(10, value);
        }

        public float MovingRange
        {
            //F3
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float FallWarpHeight
        {
            //F4
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float FalcoNumberFloat
        {
            //F5
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float ShotSpeed
        {
            //F6
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public int ShotInterval
        {
            //L7
            get => ReadInt(28);
            set => Write(28, value);
        }
    }
}
