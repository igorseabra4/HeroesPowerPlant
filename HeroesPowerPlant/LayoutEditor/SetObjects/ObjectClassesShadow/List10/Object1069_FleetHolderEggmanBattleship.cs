namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1069_FleetHolderEggmanBattleship : SetObjectShadow
    {
        public enum EggFleetType : int
        {
            BATTLE,
            MOTHER
        }

        [MiscSetting]
        public EggFleetType ShipType { get; set; }
        [MiscSetting]
        public float RangeRadius { get; set; }
        [MiscSetting]
        public float RangeHeight { get; set; }
        [MiscSetting]
        public float MoveLength { get; set; }
        [MiscSetting]
        public float MoveSpeed { get; set; }
        [MiscSetting]
        public float FireTiming { get; set; }
    }
}

