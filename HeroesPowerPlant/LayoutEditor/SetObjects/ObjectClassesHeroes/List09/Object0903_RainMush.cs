namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0903_RainMush : SetObjectHeroes
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

        public byte NoControlTime
        {
            get => ReadByte(12);
            set => Write(12, value);
        }

        public byte Model
        {
            get => ReadByte(13);
            set => Write(13, value);
        }
    }
}