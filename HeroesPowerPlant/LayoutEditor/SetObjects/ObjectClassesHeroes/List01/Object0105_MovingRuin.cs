namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectHeroes
    {
        public enum ERuinType : byte
        {
            Small = 0,
            Normal = 1,
            Special = 2
        }

        [MiscSetting]
        public ERuinType RuinType { get; set; }
        [MiscSetting]
        public float StartY { get; set; }
        [MiscSetting]
        public float Speed { get; set; }
    }
}
