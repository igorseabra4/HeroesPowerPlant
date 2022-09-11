namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0013_Balloon : SetObjectShadow
    {
        public enum EBalloonType : int
        {
            TranslationOnceAndDisappear,
            TranslationLoop,
            Orbit
        }

        [MiscSetting]
        public EBalloonType BalloonType { get; set; }
        [MiscSetting]
        public EItemShadow Item { get; set; }
        [MiscSetting]
        public float SpeedDampAmount { get; set; }
        [MiscSetting]
        public float OrbitDistance { get; set; }
        [MiscSetting]
        public float TranslationX { get; set; }
        [MiscSetting]
        public float TranslationY { get; set; }
        [MiscSetting]
        public float TranslationZ { get; set; }
    }
}
