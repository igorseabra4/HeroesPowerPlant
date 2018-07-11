namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0189_WaterfallSmall : SetObjectManagerHeroes
    {
        public byte ModelType
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public float Scale
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Speed
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}
