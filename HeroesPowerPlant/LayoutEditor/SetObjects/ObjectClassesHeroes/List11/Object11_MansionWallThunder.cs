namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_MansionWallThunder : SetObjectManagerHeroes
    {
        public int ModelNumber
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }
    }
}
