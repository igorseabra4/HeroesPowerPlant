namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001D_Pulley : SetObjectHeroes
    {
        public enum EPulleyType : short
        {
            Up = 0,
            Down = 1
        }

        [MiscSetting]
        public float Elevation { get; set; }
        [MiscSetting]
        public float ElevationAngle { get; set; }
        [MiscSetting]
        public float Power { get; set; }
        [MiscSetting]
        public EPulleyType PulleyType { get; set; }
    }
}