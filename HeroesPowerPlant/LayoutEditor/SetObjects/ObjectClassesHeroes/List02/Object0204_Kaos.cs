namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0204_Kaos : SetObjectManagerHeroes
    {
        public byte KaosNumber
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public float MinSpeed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float MaxSpeed
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Acceleration
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }
    }
}
