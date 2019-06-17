using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectManagerHeroes
    {
        public enum RuinType : byte
        {
            Small = 0,
            Normal = 1,
            Special = 2
        }

        public RuinType Type
        {
            get => (RuinType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float MovingDistance
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
