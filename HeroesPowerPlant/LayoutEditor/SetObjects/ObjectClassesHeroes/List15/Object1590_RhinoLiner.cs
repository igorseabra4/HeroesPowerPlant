using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1590_RhinoLiner : SetObjectManagerHeroes
    {
        public enum TypeEnum : byte
        {
            Standard = 0,
            Attack = 1
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public enum PathEnum : byte
        {
            Standard = 0,
            Loop = 1
        }
        public PathEnum PathMode
        {
            get { return (PathEnum)ReadByte(5); }
            set { byte a = (byte)value; Write(5, a); }
        }

        public enum IronballEnum : byte
        {
            Homing = 0,
            NotHoming = 1
        }
        public IronballEnum IronBallMode
        {
            get { return (IronballEnum)ReadByte(6); }
            set { byte a = (byte)value; Write(6, a); }
        }

        public float WeaponSpeed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public Int32 AttackInterval
        {
            get { return ReadLong(12); }
            set { Write(12, value); }
        }

        public float MoveSpeedMin
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float MoveSpeed
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public float MoveSpeedMax
        {
            get { return ReadFloat(24); }
            set { Write(24, value); }
        }
    }
}
