namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1188_Curtain : SetObjectHeroes
    {
        public enum ECurtainType : byte
        {
            Light = 0,
            Dark = 1,
        }

        [MiscSetting]
        public ECurtainType CurtainType { get; set; }
        [MiscSetting]
        public byte Pole { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsUpsideDown { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}
