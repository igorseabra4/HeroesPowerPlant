using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1104_FlameTorch : SetObjectManagerHeroes
    {
            public Int16 Unknown1
            {
                get { return ReadWriteWord(4); }
                set { ReadWriteWord(4, value); }
            }

            public bool IsBlue
            {
                get { return ReadWriteWord(6) != 0; }
                set { Int16 a = value ? (Int16)1 : (Int16)0; ReadWriteWord(6, a); }
            }

            public Int16 Unknown2
            {
                get { return ReadWriteWord(8); }
                set { ReadWriteWord(8, value); }
            }

            public enum StartModeEnum
            {
                Lit = 0,
                LitOnRange = 1,
                Unlit = 2
            }
            public StartModeEnum StartMode
            {
                get { return (StartModeEnum)ReadWriteWord(10); }
                set { Int16 a = (Int16)value; ReadWriteWord(10, a); }
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
                set { byte a = value ? (byte)1 : (byte)0; ReadWriteByte(20, a); }
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

            public bool HasCollision
            {
                get { return ReadWriteByte(22) != 0; }
                set { byte a = value ? (byte)1 : (byte)0; ReadWriteByte(22, a); }
            }

            public bool HasSE
            {
                get { return ReadWriteByte(23) != 0; }
                set { byte a = value ? (byte)1 : (byte)0; ReadWriteByte(23, a); }
            }
        }  
    }
