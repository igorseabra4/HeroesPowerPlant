namespace HeroesPowerPlant.LayoutEditor
{
    public enum Direction : byte
    {
        Up = 0,
        Down = 1,
    }

    public class Object0031_Case : SetObjectManagerHeroes
    {
        public float ScaleX
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ScaleY
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public byte LinkID
        {
            get { return ReadByte(16); }
            set { Write(16, value); }
        }

        public Direction Direction
        {
            get { return (Direction)ReadByte(17); }
            set { Write(17, (byte)value); }
        }
    }
}