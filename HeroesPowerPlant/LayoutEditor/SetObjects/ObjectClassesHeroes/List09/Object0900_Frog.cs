namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0900_Frog : SetObjectHeroes
    {
        [MiscSetting(1)]
        public float JumpDirX { get; set; }
        [MiscSetting(2)]
        public float JumpDirY { get; set; }
        [MiscSetting(3)]
        public float JumpDirZ { get; set; }
        [MiscSetting(4)]
        public float Radius { get; set; }
        [MiscSetting(5)]
        public float Scale { get; set; }
        [MiscSetting(6)]
        public float JumpCycle { get; set; }
        [MiscSetting(7)]
        public short StopTimeSec { get; set; }
        [MiscSetting(8)]
        public short LeaveTimeSec { get; set; }
        [MiscSetting(9, underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsBlack { get; set; }
    }
}