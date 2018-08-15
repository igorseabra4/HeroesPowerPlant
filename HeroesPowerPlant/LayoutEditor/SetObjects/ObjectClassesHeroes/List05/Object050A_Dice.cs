namespace HeroesPowerPlant.LayoutEditor
{
    public enum DiceType : short
    {
        UpDown = 0,
        Horizontal = 1
    }

    public class Object050A_Dice : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float Height
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Radius
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public short StopTime
        {
            get { return ReadShort(16); }
            set { Write(16, value); }
        }

        public DiceType Type
        {
            get { return (DiceType)ReadShort(18); }
            set { Write(18, (short)value); }
        }

        public short OffsetTime
        {
            get { return ReadShort(20); }
            set { Write(20, value); }
        }
    }
}