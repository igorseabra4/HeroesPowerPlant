namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0187_Tides : SetObjectHeroes
    {
        public byte TidesType
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
