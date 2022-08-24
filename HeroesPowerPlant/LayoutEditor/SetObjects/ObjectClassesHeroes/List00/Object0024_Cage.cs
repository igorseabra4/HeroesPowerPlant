namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0024_Cage : SetObjectHeroes
    {
        public enum ECageType : int
        {
            PFixed = 0,
            PFlying = 1,
            UFixed = 2,
            UFlying = 3
        }

        [MiscSetting]
        public ECageType CageType { get; set; }
        [MiscSetting]
        public float MoveCycleSec { get; set; }
        [MiscSetting]
        public float MoveRangeH { get; set; }
        [MiscSetting]
        public float MoveRangeV { get; set; }
    }
}