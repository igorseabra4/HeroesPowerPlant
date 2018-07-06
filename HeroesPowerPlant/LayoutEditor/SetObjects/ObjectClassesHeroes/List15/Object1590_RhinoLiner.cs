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
            get { return (TypeEnum)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public enum PathEnum : byte
        {
            Standard = 0,
            Loop = 1
        }
        public PathEnum PathMode
        {
            get { return (PathEnum)ReadWriteByte(5); }
            set { byte a = (byte)value; ReadWriteByte(5, a); }
        }

        public enum IronballEnum : byte
        {
            Homing = 0,
            NotHoming = 1
        }
        public IronballEnum IronBallMode
        {
            get { return (IronballEnum)ReadWriteByte(6); }
            set { byte a = (byte)value; ReadWriteByte(6, a); }
        }

        public float WeaponSpeed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public Int32 AttackInterval
        {
            get { return ReadWriteLong(12); }
            set { ReadWriteLong(12, value); }
        }

        public float MoveSpeedMin
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float MoveSpeed
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public float MoveSpeedMax
        {
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }
    }
}
