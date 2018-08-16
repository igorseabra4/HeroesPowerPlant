namespace HeroesPowerPlant.LayoutEditor
{
    public class Object01FF_SetParticle : SetObjectManagerHeroes
    {
        public byte Number
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public float SpeedX
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float SpeedY
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float SpeedZ
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float UnknownFloat
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public int UnknownInteger
        {
            get { return ReadLong(24); }
            set { Write(24, value); }
        }

        public byte UnknownByte1
        {
            get { return ReadByte(28); }
            set { Write(28, value); }
        }

        public byte UnknownByte2
        {
            get { return ReadByte(29); }
            set { Write(29, value); }
        }

        public byte UnknownByte3
        {
            get { return ReadByte(30); }
            set { Write(30, value); }
        }
    }
}
