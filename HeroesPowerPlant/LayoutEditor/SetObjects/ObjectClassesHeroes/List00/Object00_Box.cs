namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Box : SetObjectManagerHeroes
    {
        public enum CrashModeType : short
        {
            CrashOut = 0,
            CrashThrough = 1
        }
        public CrashModeType Type
        {
            get { return (CrashModeType)ReadShort(4); }
            set { Write(4, (short)value); }
        }
    }
}