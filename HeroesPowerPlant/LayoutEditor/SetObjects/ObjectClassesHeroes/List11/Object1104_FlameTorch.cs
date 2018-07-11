using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1104_FlameTorch : SetObjectManagerHeroes
    {
        public bool IsBlue
        {
            get { return ReadLong(4) != 0; }
            set { Write(4, value ? 1 : 0); }
        }
        
        public enum StartModeEnum
        {
            Lit = 0,
            LitOnRange = 1,
            Unlit = 2
        }
        public StartModeEnum StartMode
        {
            get { return (StartModeEnum)ReadLong(8); }
            set { Write(8, (int)value); }
        }

        public float Range
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Scale
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public bool IsUpsideDown
        {
            get { return ReadByte(20) != 0; }
            set { Write(20, (byte)(value ? 1 : 0)); }
        }

        public enum BaseTypeEnum
        {
            None = 0,
            Floor = 1,
            Air = 2
        }
        public BaseTypeEnum BaseType
        {
            get { return (BaseTypeEnum)ReadByte(21); }
            set { byte a = (byte)value; Write(21, a); }
        }

        public bool HasSE
        {
            get { return ReadByte(22) != 0; }
            set { Write(22, (byte)(value ? 1 : 0)); }
        }

        public bool HasCollision
        {
            get { return ReadByte(23) != 0; }
            set { Write(23, (byte)(value ? 1 : 0)); }
        }
    }
}
