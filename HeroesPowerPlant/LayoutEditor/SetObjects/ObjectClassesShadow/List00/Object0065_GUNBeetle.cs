namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0065_GUNBeetle : SetObjectShadow
    {
        public enum EAppear : int
        {
            WAIT_FLOATING,
            MOVE_ON_PATH
        }

        public enum EAction : int
        {
            NONE,
            SHOCK,
            USE_WEAPON,
        }

        public enum EWeapon : int
        {
            RIFLE,
            ROCKET4,
            BOMB,
            MACHINEGUN,
            NONE
        }

        public enum EPathType : int
        {
            LEFTRIGHT,
            UPDOWN,
            RIGHTLEFT,
            DOWNUP,
            FLY_FORWARD_UPDOWN,
            FLY_LEFT,
            FLY_FORWARD,
            FLY_FORWARD_SWOOP,
            CIRCLE
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
        public EAction ActionType { get; set; }
        [MiscSetting(9)]
        public EPathType PathType { get; set; }
        [MiscSetting(10)]
        public float PathVariable { get; set; }
        [MiscSetting(11)]
        public float AttackStart { get; set; }
        [MiscSetting(12)]
        public float AttackEnd { get; set; }
        [MiscSetting(13)]
        public ENoYes PatrolReversed { get; set; }
        [MiscSetting(14)]
        public ENoYes IsGolden { get; set; }
        [MiscSetting(15)]
        public EWeapon WeaponType { get; set; }
        [MiscSetting(16)]
        public float SparkDischarge { get; set; }
        [MiscSetting(17)]
        public float SparkWait { get; set; }
    }
}
