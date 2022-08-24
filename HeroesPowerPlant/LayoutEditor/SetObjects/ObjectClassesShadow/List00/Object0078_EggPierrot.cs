namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0078_EggPierrot : SetObjectShadow
    {
        public enum EAppear : int
        {
            WANDER,
            OFFSET
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
        public int UnknownInt { get; set; } //8 Needs Research, found 0/1 but no apparent behavior
        [MiscSetting(9)]
        public float BombTime { get; set; } //9 BakudanTime
        [MiscSetting(10)]
        public float OffsetPos_X { get; set; }
        [MiscSetting(11)]
        public float OffsetPos_Y { get; set; }
        [MiscSetting(12)]
        public float OffsetPos_Z { get; set; }
    }
}
