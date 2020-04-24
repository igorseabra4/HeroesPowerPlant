namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0026_FormGate : SetObjectHeroes
    {
        public Formation Formation
        {
            get => (Formation)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Width
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Height
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}