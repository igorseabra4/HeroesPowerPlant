namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050A_Dice : SetObjectHeroes
    {
        public enum EDiceType : short
        {
            UpDown = 0,
            Horizontal = 1
        }

        [MiscSetting]
        public float Speed { get; set; }
        [MiscSetting]
        public float Height { get; set; }
        [MiscSetting]
        public float Radius { get; set; }
        [MiscSetting]
        public short StopTime { get; set; }
        [MiscSetting]
        public EDiceType DiceType { get; set; }
        [MiscSetting]
        public short OffsetTime { get; set; }
    }
}