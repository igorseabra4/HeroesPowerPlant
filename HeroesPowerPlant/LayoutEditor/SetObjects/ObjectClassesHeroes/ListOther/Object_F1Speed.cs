namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Speed : SetObjectHeroes
    {
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}