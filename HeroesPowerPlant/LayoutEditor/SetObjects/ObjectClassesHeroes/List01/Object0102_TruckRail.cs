namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0102_TruckRail : SetObjectHeroes
    {
        public byte TruckRailType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public byte StopTime
        {
            get => ReadByte(5);
            set => Write(4, value);
        }

        public float Length
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
