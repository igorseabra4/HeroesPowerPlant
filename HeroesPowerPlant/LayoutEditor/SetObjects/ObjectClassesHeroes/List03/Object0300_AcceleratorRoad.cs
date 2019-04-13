namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0300_AcceleratorRoad : SetObjectManagerHeroes
    {
        public byte Type
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ColliZAdd
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}