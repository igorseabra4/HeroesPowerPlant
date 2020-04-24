namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_02 : SetObjectHeroes
    {
        public float CoreHP
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ShieldHP
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MissleHP
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
