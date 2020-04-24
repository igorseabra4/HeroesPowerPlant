namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0103_TruckPath : SetObjectHeroes
    {
        public byte TruckPathType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public byte PathNum
        {
            get => ReadByte(5);
            set => Write(5, value);
        }

        public float MinSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
