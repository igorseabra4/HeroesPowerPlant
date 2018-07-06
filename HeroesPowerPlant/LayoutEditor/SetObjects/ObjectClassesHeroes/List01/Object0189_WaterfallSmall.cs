namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0189_WaterfallSmall : SetObjectManagerHeroes
    {
        public byte ModelType
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}
