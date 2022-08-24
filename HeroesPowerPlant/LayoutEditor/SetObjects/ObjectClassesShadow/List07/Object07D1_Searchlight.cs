namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D1_Searchlight : SetObjectShadow
    {
        // ElecSearchLight(ENEMY_ID, RotateRange, RotateSpeed, LightLength)
        [MiscSetting]
        public ENoYes SpotOnLinkID { get; set; }
        [MiscSetting]
        public float RotateRange { get; set; }
        [MiscSetting]
        public float RotateSpeed { get; set; }
        [MiscSetting]
        public float LightLength { get; set; }
    }
}

