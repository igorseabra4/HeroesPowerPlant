namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1102_CastleWall : SetObjectManagerHeroes
    {
        public int ModelNumber
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }

        public bool IsUpsideDown
        {
            get { return ReadLong(8) != 0; }
            set { Write(8, value ? 1 : 0); }
        }
    }
}
