namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0908_RainFruit : SetObjectHeroes
    {
        public float Range
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Power
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public short NoControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }

        public short FruitType
        {
            get => ReadShort(14);
            set => Write(14, value);
        }

        public short DeleteTimeSec
        {
            get => ReadShort(16);
            set => Write(16, value);
        }
    }
}