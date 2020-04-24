namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0014_GoalRing : SetObjectShadow
    {
        public EmeraldColor EmeraldType
        {
            get => (EmeraldColor)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum EmeraldColor
    {
        Blue = 0,
        Green = 1,
        Purple = 2,
        Red = 3,
        Aqua = 4,
        Yellow = 5,
        White = 6,
        None = 7
    }
}
