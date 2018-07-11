namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0204_Kaos : SetObjectManagerHeroes
    {
        public byte KaosNumber
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public float MinSpeed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float MaxSpeed
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Acceleration
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }
    }
}
