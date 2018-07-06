namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002E_Fan : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float HeightTriangleDive
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float HeightDefault
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Power
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public enum FanMode : byte
        {
            Normal = 0,
            Switchable = 1,
            Normal2 = 2,
            Switchable2 = 3
        }

        public FanMode Mode
        {
            get { return (FanMode)ReadWriteByte(20); }
            set { byte a = (byte)value; ReadWriteByte(20, a); }
        }

        public byte LinkID
        {
            get { return ReadWriteByte(21); }
            set { ReadWriteByte(21, value); }
        }

        public float WindScale
        {
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }

        public bool IsInvisible
        {
            get { return ReadWriteByte(28) != 0; }
            set { ReadWriteByte(28, value ? (byte)1 : (byte)0); }
        }
    }
}