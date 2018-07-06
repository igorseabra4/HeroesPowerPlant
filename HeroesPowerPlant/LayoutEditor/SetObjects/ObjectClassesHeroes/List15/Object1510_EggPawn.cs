using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1510_EggPawn : SetObjectManagerHeroes
    {
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
            get { return (StartModeEnum)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
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
            get { return (MassTypeEnum)ReadWriteByte(5); }
            set { byte a = (byte)value; ReadWriteByte(5, a); }
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
            get { return (WeaponTypeEnum)ReadWriteByte(6); }
            set { byte a = (byte)value; ReadWriteByte(6, a); }
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
            get { return (ShieldTypeEnum)ReadWriteByte(7); }
            set { byte a = (byte)value; ReadWriteByte(7, a); }
        }

        public Int16 ScopeRange
        {
            //W2
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public Int16 ScopeOffset
        {
            //W2
            get { return ReadWriteWord(10); }
            set { ReadWriteWord(10, value); }
        }

        public float MovingRange
        {
            //F3
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float FallWarpHeight
        {
            //F4
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float FalcoNumberFloat
        {
            //F5
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public float ShotSpeed
        {
            //F6
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }

        public Int32 ShotInterval
        {
            //L7
            get { return ReadWriteLong(28); }
            set { ReadWriteLong(28, value); }
        }
    }
}
