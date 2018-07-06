using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Box : SetObjectManagerHeroes
    {
        public enum CrashModeType : UInt16
        {
            CrashOut = 0,
            CrashThrough = 1
        }
        public CrashModeType Type
        {
            get { return (CrashModeType)ReadWriteWord(4); }
            set { Int16 a = (Int16)value; ReadWriteWord(4, a); }
        }
    }
}