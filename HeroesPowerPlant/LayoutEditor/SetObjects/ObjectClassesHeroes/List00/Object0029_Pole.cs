namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0029_Pole : SetObjectHeroes
    {
        [MiscSetting]
        public short Length { get; set; }
        [MiscSetting]
        public short Range { get; set; }
        [MiscSetting]
        public short StartOffset { get; set; }
        [MiscSetting]
        public short EndOffset { get; set; }
        [MiscSetting]
        public float ReleaseElevation { get; set; }
        [MiscSetting]
        public float ReleaseAzimuth { get; set; }
        [MiscSetting]
        public float ReleasePower { get; set; }
        [MiscSetting]
        public byte UseReference { get; set; }
        [MiscSetting]
        public byte ReferenceID { get; set; }
        [MiscSetting]
        public short NoControlTime { get; set; }
    }
}