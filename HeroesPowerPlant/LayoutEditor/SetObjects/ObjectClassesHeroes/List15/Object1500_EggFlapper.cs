using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1500_EggFlapper : SetObjectManagerHeroes
    {
        public enum QualityType : byte
        {
            Normal = 0,
            Silver = 1
        }
        public QualityType Quality
        {
            get { return (QualityType)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public enum ShadowEnum : byte
        {
            Auto = 0,
            Set = 1,
            SetWithoutShadow = 2
        }
        public ShadowEnum ShadowType
        {
            get { return (ShadowEnum)ReadWriteByte(5); }
            set { byte a = (byte)value; ReadWriteByte(5, a); }
        }

        public enum MoveEnum : byte
        {
            Wait = 0,
            BackAndForth = 1,
            Wander = 2,
            Idle = 3
        }
        public MoveEnum MoveType
        {
            get { return (MoveEnum)ReadWriteByte(6); }
            set { byte a = (byte)value; ReadWriteByte(6, a); }
        }

        public enum WeaponEnum : byte
        {
            None = 0,
            Needle = 1,
            Shot = 2,
            MGun = 3,
            Laser = 4,
            Bomb = 5,
            Searchlight = 6
        }
        public WeaponEnum WeaponType
        {
            get { return (WeaponEnum)ReadWriteByte(7); }
            set { byte a = (byte)value; ReadWriteByte(7, a); }
        }

        public Int16 ScopeRange
        {
            //21
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public Int16 ScopeOffset
        {
            //22
            get { return ReadWriteWord(10); }
            set { ReadWriteWord(10, value); }
        }

        public Int16 AttackInterval
        {
            //31
            get { return ReadWriteWord(12); }
            set { ReadWriteWord(12, value); }
        }

        public Int16 AttackFrame
        {
            //31
            get { return ReadWriteWord(14); }
            set { ReadWriteWord(14, value); }
        }

        public float FLOOR
        {
            //4
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float MoveSpeed
        {
            //5
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public float MoveRange
        {
            //6
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }

        public float WeaponSpeed
        {
            //7
            get { return ReadWriteSingle(28); }
            set { ReadWriteSingle(28, value); }
        }

        public Int16 LightAngleY
        {
            //W8
            get { return ReadWriteWord(32); }
            set { ReadWriteWord(32, value); }
        }

        public Int16 LightAngleX
        {
            //W8
            get { return ReadWriteWord(34); }
            set { ReadWriteWord(34, value); }
        }
    }
}
