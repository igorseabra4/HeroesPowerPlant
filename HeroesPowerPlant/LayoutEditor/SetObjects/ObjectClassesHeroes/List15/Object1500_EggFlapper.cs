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
            get { return (QualityType)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public enum ShadowEnum : byte
        {
            Auto = 0,
            Set = 1,
            SetWithoutShadow = 2
        }
        public ShadowEnum ShadowType
        {
            get { return (ShadowEnum)ReadByte(5); }
            set { byte a = (byte)value; Write(5, a); }
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
            get { return (MoveEnum)ReadByte(6); }
            set { byte a = (byte)value; Write(6, a); }
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
            get { return (WeaponEnum)ReadByte(7); }
            set { byte a = (byte)value; Write(7, a); }
        }

        public Int16 ScopeRange
        {
            //21
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public Int16 ScopeOffset
        {
            //22
            get { return ReadShort(10); }
            set { Write(10, value); }
        }

        public Int16 AttackInterval
        {
            //31
            get { return ReadShort(12); }
            set { Write(12, value); }
        }

        public Int16 AttackFrame
        {
            //31
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

        public Int16 LightAngleY
        {
            //W8
            get { return ReadShort(32); }
            set { Write(32, value); }
        }

        public Int16 LightAngleX
        {
            //W8
            get { return ReadShort(34); }
            set { Write(34, value); }
        }
    }
}
