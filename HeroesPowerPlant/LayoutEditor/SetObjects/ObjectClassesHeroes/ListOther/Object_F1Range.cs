namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Range : SetObjectHeroes
    {
        public float Range
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}