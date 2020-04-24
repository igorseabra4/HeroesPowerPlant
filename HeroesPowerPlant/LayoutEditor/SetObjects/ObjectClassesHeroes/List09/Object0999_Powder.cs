namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0999_Powder : SetObjectHeroes
    {
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int ObjectType
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public int Number
        {
            get => ReadInt(12);
            set => Write(12, value);
        }

        public int Time
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public float RangeR
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float RangeY
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }
}