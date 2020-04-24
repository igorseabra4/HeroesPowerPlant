namespace HeroesPowerPlant.LayoutEditor
{
    public enum DiceType : short
    {
        UpDown = 0,
        Horizontal = 1
    }

    public class Object050A_Dice : SetObjectHeroes
    {
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Height
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Radius
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public short StopTime
        {
            get => ReadShort(16);
            set => Write(16, value);
        }

        public DiceType DiceType
        {
            get => (DiceType)ReadShort(18);
            set => Write(18, (short)value);
        }

        public short OffsetTime
        {
            get => ReadShort(20);
            set => Write(20, value);
        }
    }
}