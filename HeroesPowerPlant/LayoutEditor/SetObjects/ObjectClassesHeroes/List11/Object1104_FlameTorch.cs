using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1104_FlameTorch : SetObjectManagerHeroes
    {
        public bool IsBlue
        {
            get { return ReadWriteLong(4) != 0; }
            set { ReadWriteLong(4, value ? 1 : 0); }
        }
        
        public enum StartModeEnum
        {
            Lit = 0,
            LitOnRange = 1,
            Unlit = 2
        }
        public StartModeEnum StartMode
        {
            get { return (StartModeEnum)ReadWriteLong(8); }
            set { ReadWriteLong(8, (int)value); }
        }

        public float Range
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public bool IsUpsideDown
        {
            get { return ReadWriteByte(20) != 0; }
            set { ReadWriteByte(20, (byte)(value ? 1 : 0)); }
        }

        public enum BaseTypeEnum
        {
            None = 0,
            Floor = 1,
            Air = 2
        }
        public BaseTypeEnum BaseType
        {
            get { return (BaseTypeEnum)ReadWriteByte(21); }
            set { byte a = (byte)value; ReadWriteByte(21, a); }
        }

        public bool HasSE
        {
            get { return ReadWriteByte(22) != 0; }
            set { ReadWriteByte(22, (byte)(value ? 1 : 0)); }
        }

        public bool HasCollision
        {
            get { return ReadWriteByte(23) != 0; }
            set { ReadWriteByte(23, (byte)(value ? 1 : 0)); }
        }
    }
}
