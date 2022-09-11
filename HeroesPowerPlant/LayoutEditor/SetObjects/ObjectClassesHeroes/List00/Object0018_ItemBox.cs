namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0018_ItemBox : SetObjectHeroes
    {
        [MiscSetting]
        public EItemHeroes Item { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool HomingOff { get; set; }
    }
}