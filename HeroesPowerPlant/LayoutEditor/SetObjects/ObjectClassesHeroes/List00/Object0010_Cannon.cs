namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Cannon : SetObjectHeroes
    {
        [MiscSetting]
        public short SpeedElevation { get; set; }
        [MiscSetting]
        public short SpeedAzimuth { get; set; }
        [MiscSetting]
        public short SpeedNoControlTime { get; set; }
        [MiscSetting]
        public short SpeedPower { get; set; }

        [MiscSetting]
        public short FlyElevation { get; set; }
        [MiscSetting]
        public short FlyAzimuth { get; set; }
        [MiscSetting]
        public short FlyNoControlTime { get; set; }
        [MiscSetting]
        public short FlyPower { get; set; }

        [MiscSetting]
        public short PowerElevation { get; set; }
        [MiscSetting]
        public short PowerAzimuth { get; set; }
        [MiscSetting]
        public short PowerNoControlTime { get; set; }
        [MiscSetting]
        public short PowerPower { get; set; }
    }
}