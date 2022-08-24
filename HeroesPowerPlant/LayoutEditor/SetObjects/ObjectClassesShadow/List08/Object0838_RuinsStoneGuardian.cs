namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0838_RuinsStoneGuardian : SetObjectShadow
    {
        public enum EOffOn : int
        {
            Off,
            On
        }

        //StoneStatue(Active{off,on},Mirror{off,on},Range_Radius m, Range_height m)
        [MiscSetting]
        public EOffOn Active { get; set; }
        [MiscSetting]
        public EOffOn Mirror { get; set; }
        [MiscSetting]
        public float DetectRadius { get; set; }
        [MiscSetting]
        public float DetectHeight { get; set; }
    }
}
