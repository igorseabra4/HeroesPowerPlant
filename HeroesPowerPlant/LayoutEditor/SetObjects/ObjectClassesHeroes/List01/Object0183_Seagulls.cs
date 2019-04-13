namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0183_Seagulls : SetObjectManagerHeroes
    {
        public byte Number
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Radius
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
