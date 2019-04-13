namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_MansionWallThunder : SetObjectManagerHeroes
    {
        public int ModelNumber
        {
            get => ReadLong(4);
            set => Write(4, value);
        }
    }
}
