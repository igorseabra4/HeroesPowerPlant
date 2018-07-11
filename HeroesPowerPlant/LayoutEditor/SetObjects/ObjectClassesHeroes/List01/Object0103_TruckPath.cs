namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0103_TruckPath : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public byte PathNum
        {
            get { return ReadByte(5); }
            set { Write(4, value); }
        }

        public float MinSpeed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}
