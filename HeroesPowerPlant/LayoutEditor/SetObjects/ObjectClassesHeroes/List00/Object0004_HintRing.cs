namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectHeroes
    {
        [MiscSetting]
        public short LineToPlay { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool DeleteByLinkOff { get; set; }
    }
}