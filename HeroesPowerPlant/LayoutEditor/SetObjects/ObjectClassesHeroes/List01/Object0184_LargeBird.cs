namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0184_LargeBird : SetObjectHeroes
    {
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Scale
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
