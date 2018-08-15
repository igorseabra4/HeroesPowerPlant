namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0029_Pole : SetObjectManagerHeroes
    {
        public short Length
        {
            get { return ReadShort(4); }
            set { Write(4, value); }
        }

        public short Range
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public short StartOffset
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public short EndOffset
        {
            get { return ReadShort(10); }
            set { Write(10, value); }
        }

        public float ReleaseElevation
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float ReleaseAzimuth
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float ReleasePower
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public bool UseReference
        {
            get { return ReadByte(24) != 0; }
            set { Write(24, (byte)(value ? 1 : 0)); }
        }

        public byte ReferenceID
        {
            get { return ReadByte(25); }
            set { Write(25, value); }
        }

        public short NoControlTime
        {
            get { return ReadShort(26); }
            set { Write(26, value); }
        }
    }
}