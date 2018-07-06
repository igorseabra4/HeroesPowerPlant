using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1103_CastleFloatingPlatform : SetObjectManagerHeroes
    {
            public enum PlatformTypeEnum
            {
                Fixed = 0,
                Moving = 1,
                Alternate = 2,
                Disappear = 3
            }
            public PlatformTypeEnum PlatformType
            {
                get { return (PlatformTypeEnum)ReadWriteByte(4); }
                set { byte a = (byte)value; ReadWriteByte(4, a); }
            }

            public bool IsUpsideDown
            {
                get { return ReadWriteByte(5) != 0; }
                set { byte a = value ? (byte)1 : (byte)0; ReadWriteByte(5, a); }
            }

            public Int16 UnknownAlternateRange0
            {
                get { return ReadWriteWord(6); }
                set { ReadWriteWord(8, value); }
            }

            public Int16 UnknownAlternateRange1
            {
                get { return ReadWriteWord(8); }
                set { ReadWriteWord(8, value); }
            }

            public Int16 XOffset
            {
                get { return ReadWriteWord(10); }
                set { ReadWriteWord(10, value); }
            }

            public Int16 YOffset
            {
                get { return ReadWriteWord(12); }
                set { ReadWriteWord(12, value); }
            }

            public Int16 ZOffset
            {
                get { return ReadWriteWord(14); }
                set { ReadWriteWord(14, value); }
            }

            public Int16 TimeCycleFrame
            {
                get { return ReadWriteWord(16); }
                set { ReadWriteWord(16, value); }
            }

            public byte DisappearLinkID
            {
                get { return ReadWriteByte(18); }
                set { ReadWriteByte(18, value); }
            }

            public Int16 Unknown
            {
                get { return ReadWriteWord(20); }
                set { ReadWriteWord(20, value); }
            }
        }
    }
