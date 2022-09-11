namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0093_BkNinja : SetObjectShadow
    {
        public enum EAppear : int
        {
            STAND,
            APPEAR_WHEN_CLOSE,
            WARP,
            ON_AIR_SAUCER_WARP
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
        public int ShootCount { get; set; }
        [MiscSetting(9)]
        public float AttackInterval { get; set; }
        [MiscSetting(10)]
        public float WaitInterval { get; set; }
        [MiscSetting(11)]
        public float Pos0_X { get; set; }
        [MiscSetting(12)]
        public float Pos0_Y { get; set; }
        [MiscSetting(13)]
        public float Pos0_Z { get; set; }
        [MiscSetting(14)]
        public int UNUSED_Pos0_IntWaitType { get; set; }
        [MiscSetting(15)]
        public float UNUSED_Pos0_DisappearTime { get; set; }
        [MiscSetting(16)]
        public float UNUSED_Pos1_X { get; set; }
        [MiscSetting(17)]
        public float UNUSED_Pos1_Y { get; set; }
        [MiscSetting(18)]
        public float UNUSED_Pos1_Z { get; set; }
        [MiscSetting(19)]
        public float UNUSED_Pos1_WaitType { get; set; }
        [MiscSetting(20)]
        public float UNUSED_Pos1_DisappearTime { get; set; }
        [MiscSetting(21)]
        public float UNUSED_Float21 { get; set; }
        [MiscSetting(22)]
        public float UNUSED_Float22 { get; set; }
    }
}
