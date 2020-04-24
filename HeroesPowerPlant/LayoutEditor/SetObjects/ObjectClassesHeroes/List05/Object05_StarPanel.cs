namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_StarPanel : SetObjectHeroes
    {
        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int Color
        {
            get => ReadInt(8);
            set => Write(8, value);
        }
    }
}