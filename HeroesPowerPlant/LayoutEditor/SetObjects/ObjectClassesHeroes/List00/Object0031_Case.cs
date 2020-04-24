using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum Direction : byte
    {
        Up = 0,
        Down = 1,
    }

    public class Object0031_Case : SetObjectHeroes
    {

        public float ScaleX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public byte LinkID
        {
            get => ReadByte(16);
            set => Write(16, value);
        }

        public Direction Direction
        {
            get => (Direction)ReadByte(17);
            set => Write(17, (byte)value);
        }
    }
}