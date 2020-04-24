namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_Spring : SetObjectHeroes
    {
        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float RotSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}