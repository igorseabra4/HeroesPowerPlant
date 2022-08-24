namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0068_GUNRobot : SetObjectShadow
    {
        public enum EBody : int
        {
            Trooper,
            GigaTrooper
        }

        public enum EAppear : int
        {
            WAIT_ACT,
            OFFSET,
            WARP,
            XXX
        }

        public enum EWeapon : int
        {
            AUTORIFLE,
            AIRCRAFTRIFLE,
            BAZOOKA,
            ROCKET4,
            ROCKET8,
            LASERRIFLE
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
        public EBody BodyType { get; set; }
        [MiscSetting(10)]
        public float OffsetPos_X { get; set; }
        [MiscSetting(11)]
        public float OffsetPos_Y { get; set; }
        [MiscSetting(12)]
        public float OffsetPos_Z { get; set; }
        [MiscSetting(13)]
        public EWaitActMove WaitActMoveType { get; set; }
        [MiscSetting(14)]
        public float Pos0_ActionTime { get; set; }
        [MiscSetting(15)]
        public EAction Pos0_ActionType { get; set; }
        [MiscSetting(16)]
        public float Pos0_MoveSpeedRatio { get; set; }
        [MiscSetting(17)]
        public float Pos0_TranslationXFromOrigin { get; set; }
        [MiscSetting(18)]
        public float Pos0_TranslationZFromOrigin { get; set; }
        [MiscSetting(19)]
        public float Pos1_ActionTime { get; set; }
        [MiscSetting(20)]
        public EAction Pos1_ActionType { get; set; }
        [MiscSetting(21)]
        public float Pos1_MoveSpeedRatio { get; set; }
        [MiscSetting(22)]
        public float Pos1_TranslationXFromOrigin { get; set; }
        [MiscSetting(23)]
        public float Pos1_TranslationZFromOrigin { get; set; }
        [MiscSetting(24)]
        public float Pos2_ActionTime { get; set; }
        [MiscSetting(25)]
        public EAction Pos2_ActionType { get; set; }
        [MiscSetting(26)]
        public float Pos2_MoveSpeedRatio { get; set; }
    }
}
