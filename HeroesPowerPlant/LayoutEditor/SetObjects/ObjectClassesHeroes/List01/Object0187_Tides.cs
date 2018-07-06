namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0187_Tides : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}
