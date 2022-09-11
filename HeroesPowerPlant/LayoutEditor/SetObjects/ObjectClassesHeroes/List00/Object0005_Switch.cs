namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0005_Switch : SetObjectHeroes
    {
        public enum ESwitchType : byte
        {
            Alternate = 0,
            Touch = 1,
            Once = 2,
            Interlock = 3
        }

        public enum ESound : byte
        {
            Pi = 0,
            Pipori = 1
        }

        [MiscSetting]
        public ESwitchType SwitchType { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool Hidden { get; set; }
        [MiscSetting]
        public byte LinkIDforHidden { get; set; }
        [MiscSetting]
        public ESound Sound { get; set; }
    }
}