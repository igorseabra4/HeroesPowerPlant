namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0091_BkLarva : SetObjectShadow
    {
        public enum EAppear : int
        {
            NORMAL,
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
        public int NumberOfLarva { get; set; }
        [MiscSetting(8)]
        public float AppearRange { get; set; }
        [MiscSetting(9)]
        public EAppear AppearType { get; set; }
        [MiscSetting(10)]
        public float Offset_Y { get; set; }
    }
}
