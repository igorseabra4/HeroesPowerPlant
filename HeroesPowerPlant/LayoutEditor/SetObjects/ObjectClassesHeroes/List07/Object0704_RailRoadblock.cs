namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0704_RailRoadblock : SetObjectHeroes
    {
        public float Range
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int Speed
        {
            get => ReadInt(8);
            set => Write(8, value);
        }
    }
}