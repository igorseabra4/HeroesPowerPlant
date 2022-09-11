namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D4_ElecSecurity : SetObjectShadow
    {
        public enum EElecSecurityType : int
        {
            STRONG,
            MEDIUM,
            WEAK,
            TIKAL,
        }

        [MiscSetting]
        public EElecSecurityType ElecSecurityType { get; set; }
        [MiscSetting]
        public float DetectionRange { get; set; }
    }
}
