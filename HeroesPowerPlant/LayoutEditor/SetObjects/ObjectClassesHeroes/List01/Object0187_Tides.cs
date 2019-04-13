namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0187_Tides : SetObjectManagerHeroes
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
    }
}
