namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0102_TruckRail : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public byte StopTime
        {
            get { return ReadWriteByte(5); }
            set { ReadWriteByte(4, value); }
        }

        public float Lenght
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}
