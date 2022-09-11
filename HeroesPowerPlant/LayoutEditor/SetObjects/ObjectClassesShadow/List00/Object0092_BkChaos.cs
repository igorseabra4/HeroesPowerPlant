namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0092_BkChaos : SetObjectShadow
    {
        public enum EState : int
        {
            BrokenUp,
            Complete
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

        // end EnemyBase

        [MiscSetting(6)]
        public float Health { get; set; }
        [MiscSetting(7)]
        public EState StartingState { get; set; }
        [MiscSetting(8)]
        public int NumberOfChibi { get; set; }
        [MiscSetting(9)]
        public float BrokenPieceFlyDistance { get; set; }
        [MiscSetting(10)]
        public float BrokenPieceFlyOffset { get; set; }
        [MiscSetting(11)]
        public float CombineStartTime { get; set; }
        [MiscSetting(12)]
        public float CombineTime { get; set; }
    }
}
