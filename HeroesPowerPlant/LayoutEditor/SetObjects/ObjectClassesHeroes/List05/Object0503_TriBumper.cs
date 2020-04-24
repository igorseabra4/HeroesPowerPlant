namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0503_TriBumper : SetObjectHeroes
    {
        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public byte AngType
        {
            get => ReadByte(8);
            set => Write(8, value);
        }

        public byte Color
        {
            get => ReadByte(9);
            set => Write(9, value);
        }

        public byte Number
        {
            get => ReadByte(10);
            set => Write(10, value);
        }
    }
}