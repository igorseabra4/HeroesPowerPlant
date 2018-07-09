namespace HeroesPowerPlant.LayoutEditor
{
    public class Object04_CraneWallLight : SetObjectManagerHeroes
    {
        public short RotSpeed
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteWord(4, value); }
        }
    }
}
