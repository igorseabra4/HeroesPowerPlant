namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0204_Kaos : SetObjectHeroes
    {
        public byte KaosNumber
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float MinSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MaxSpeed
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Acceleration
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}
