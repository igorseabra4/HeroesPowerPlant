using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum MovingRuinType : byte
    {
        Small = 0,
        Normal = 1,
        Special = 2
    }

    public class Object0105_MovingRuin : SetObjectHeroes
    {
        public MovingRuinType RuinType
        {
            get => (MovingRuinType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float StartY
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
