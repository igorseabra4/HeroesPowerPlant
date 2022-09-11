namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0014_GoalRing : SetObjectShadow
    {
        public enum EEmeraldColor : int
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

        [MiscSetting]
        public EEmeraldColor EmeraldColor { get; set; }
    }
}
