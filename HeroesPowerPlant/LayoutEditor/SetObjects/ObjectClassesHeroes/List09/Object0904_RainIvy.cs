namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0904_RainIvy : SetObjectHeroes
    {
        public float Range
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float MotionSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public int NotInUse
        {
            get => ReadInt(12);
            set => Write(12, value);
        }
    }
}