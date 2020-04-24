namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0401_EnergyColumn : SetObjectHeroes
    {
        public byte EnergyColumnType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Lenght
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Speed
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
