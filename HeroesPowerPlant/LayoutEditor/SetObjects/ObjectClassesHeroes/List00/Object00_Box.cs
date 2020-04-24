namespace HeroesPowerPlant.LayoutEditor
{
    public enum CrashMode : short
    {
        CrashOut = 0,
        CrashThrough = 1
    }
    public class Object00_Box : SetObjectHeroes
    {
        public CrashMode CrashMode
        {
            get => (CrashMode)ReadShort(4);
            set => Write(4, (short)value);
        }
    }
}