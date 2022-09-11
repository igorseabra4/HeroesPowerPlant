namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002E_Fan : SetObjectHeroes
    {
        public enum EFanMode : byte
        {
            Normal = 0,
            Switchable = 1,
            Normal2 = 2,
            Switchable2 = 3
        }

        [MiscSetting]
        public float Scale { get; set; }
        [MiscSetting]
        public float HeightTriangleDive { get; set; }
        [MiscSetting]
        public float HeightDefault { get; set; }
        [MiscSetting]
        public float Power { get; set; }
        [MiscSetting]
        public EFanMode Mode { get; set; }
        [MiscSetting]
        public byte LinkID { get; set; }
        [MiscSetting]
        public float WindScale { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsInvisible { get; set; }
    }
}