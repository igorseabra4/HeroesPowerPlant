namespace HeroesPowerPlant.LayoutEditor
{
    public class Object008E_BkWingLarge : SetObjectShadow
    {
        public enum EAppear : int
        {
            WAIT_FLOATING,
            MOVE_ON_PATH
        }

        public enum EAction : int
        {
            NONE,
            AIR_CUTTER
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

        public enum EBodyAndDeathType : int
        {
            BLACK_HAWK_DISAPPEAR_ON_KILL = 0,
            BLACK_HAWK_FALL_ON_KILL = 1,
            BLACK_VOLT_DISAPPEAR_ON_KILL = 16,
            BLACK_VOLT_FALL_ON_KILL = 17
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
        public EBodyAndDeathType BodyAndDeathType { get; set; }
    }
}
