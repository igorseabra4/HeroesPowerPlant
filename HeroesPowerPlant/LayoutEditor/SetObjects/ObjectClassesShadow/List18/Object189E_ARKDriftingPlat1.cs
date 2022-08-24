namespace HeroesPowerPlant.LayoutEditor
{
    public class Object189E_ARKDriftingPlat1 : SetObjectShadow
    {
        public enum EType : int
        {
            Burst,
            Hover
        }

        //FootingBreak(Type{Burst,Hover}, WaitSec, HoverLength point, HoverSec, MoveLengthX point, MoveLengthY point, MoveLengthZ point, MoveSec)
        [MiscSetting]
        public EType PlatformType { get; set; }
        [MiscSetting]
        public float ExplosionDelay { get; set; }
        [MiscSetting]
        public float HoverLength { get; set; }
        [MiscSetting]
        public float HoverSec { get; set; }
        [MiscSetting]
        public float TranslationX { get; set; }
        [MiscSetting]
        public float TranslationY { get; set; }
        [MiscSetting]
        public float TranslationZ { get; set; }
        [MiscSetting]
        public float TravelTime { get; set; }
    }
}
