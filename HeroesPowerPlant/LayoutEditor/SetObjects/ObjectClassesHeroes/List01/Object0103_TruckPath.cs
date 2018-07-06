namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0103_TruckPath : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public byte PathNum
        {
            get { return ReadWriteByte(5); }
            set { ReadWriteByte(4, value); }
        }

        public float MinSpeed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}
