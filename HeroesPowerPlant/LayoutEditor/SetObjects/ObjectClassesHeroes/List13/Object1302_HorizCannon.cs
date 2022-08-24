namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1302_HorizCannon : SetObjectHeroes
    {
        [MiscSetting]
        public short ShootTime { get; set; }
        [MiscSetting]
        public float ShootRange { get; set; }
        [MiscSetting]
        public byte IgnoreCollision { get; set; }
    }
}