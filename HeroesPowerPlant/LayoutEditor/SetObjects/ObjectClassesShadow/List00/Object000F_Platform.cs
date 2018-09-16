namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_Platform : SetObjectManagerShadow
    {
        public int Type
        {
            get => ReadInt(0);
            set => Write(0, value);
        }

        public int Unknown_04
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float TravelTime
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float WaitTime
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public int Unknown_10
        {
            get => ReadInt(0x10);
            set => Write(0x10, value);
        }

        public int Unknown_14
        {
            get => ReadInt(0x14);
            set => Write(0x14, value);
        }

        public float TranslationX
        {
            get { return ReadFloat(0x18); }
            set { Write(0x18, value); }
        }

        public float TranslationY
        {
            get { return ReadFloat(0x1C); }
            set { Write(0x1C, value); }
        }

        public float TranslationZ
        {
            get { return ReadFloat(0x20); }
            set { Write(0x20, value); }
        }

        public int Unknown_24
        {
            get => ReadInt(0x24);
            set => Write(0x24, value);
        }
    }
}
