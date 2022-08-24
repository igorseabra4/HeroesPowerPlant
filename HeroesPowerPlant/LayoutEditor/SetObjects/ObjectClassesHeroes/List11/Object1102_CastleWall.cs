namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1102_CastleWall : SetObjectHeroes
    {
        [MiscSetting]
        public int ObjectType { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsUpsideDown { get; set; }
    }
}
