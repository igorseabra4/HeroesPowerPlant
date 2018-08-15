namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Weight : SetObjectManagerHeroes
    {
        public enum WeightType
        {
            Repeat = 0,
            Shadow = 1,
            Laser = 2,
            RepeatSwitch = 3,
            ShadowSwitch = 4,
            LaserSwitch = 5
        }

        public WeightType Type
        {
            get { return (WeightType)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public byte LinkID
        {
            get { return ReadByte(5); }
            set { Write(5, value); }
        }

        public short Height
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public float ScaleX
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public short UpWaitTime
        {
            get { return ReadShort(16); }
            set { Write(16, value); }
        }

        public short DownWaitTime
        {
            get { return ReadShort(18); }
            set { Write(18, value); }
        }

        public float ScaleY
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }
    }
}