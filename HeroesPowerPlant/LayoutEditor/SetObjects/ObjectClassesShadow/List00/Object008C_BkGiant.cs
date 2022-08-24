namespace HeroesPowerPlant.LayoutEditor
{
    public class Object008C_BkGiant : SetObjectShadow
    {
        public enum EWeapon : int
        {
            BLACK_SWORD,
            DARK_HAMMER,
            BIG_BARREL
        }

        public enum EAppear : int
        {
            WAIT,
            DROP
        }

        // EnemyBase
        [MiscSetting(0)]
        public float MoveRange { get; set; }
        [MiscSetting(1)]
        public float SearchRange { get; set; }
        [MiscSetting(2)]
        public float SearchAngle { get; set; }
        [MiscSetting(3)]
        public float SearchWidth { get; set; }
        [MiscSetting(4)]
        public float SearchHeight { get; set; }
        [MiscSetting(5)]
        public float SearchHeightOffset { get; set; }
        [MiscSetting(6)]
        public float MoveSpeedRatio { get; set; }
        // end EnemyBase
        [MiscSetting(7)]
        public EAppear AppearType { get; set; }
        [MiscSetting(8)]
        public EWeapon WeaponType { get; set; }
        [MiscSetting(9)]
        public float OffsetPos_Y { get; set; }
        [MiscSetting(10)]
        public ENoYes CanBlockShots { get; set; }
    }
}
