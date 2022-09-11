namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C80_BounceBall : SetObjectShadow
    {
        public enum EAppearType : int
        {
            Always,
            AfterLinkIDCleared
        }

        //CircusBall [Horror/Circus skins] (type, level, nocontrol time (sec), angle)
        [MiscSetting]
        public EAppearType AppearType { get; set; }
        [MiscSetting]
        public float Strength { get; set; }
        [MiscSetting]
        public float NoControlTime { get; set; }
        [MiscSetting]
        public float Angle { get; set; }
    }
}
