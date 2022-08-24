using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_FloatingPlatform : SetObjectHeroes
    {
        public enum EPlatformType : byte
        {
            Fixed = 0,
            Moving = 1,
            Alternate = 2,
            Disappear = 3
        }

        [MiscSetting(1), Description("Disappear mode is unused. Delay can be negative to make the platform faster.")]
        public EPlatformType PlatformType { get; set; }
        [MiscSetting(2, underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool AlternateModel { get; set; }
        [MiscSetting(3)]
        public short UnknownAlternateRange0 { get; set; }
        [MiscSetting(4)]
        public short UnknownAlternateRange1 { get; set; }
        [MiscSetting(5)]
        public short XOffset { get; set; }
        [MiscSetting(6)]
        public short YOffset { get; set; }
        [MiscSetting(7)]
        public short ZOffset { get; set; }
        [MiscSetting(8)]
        public short TimeCycleFrame { get; set; }
        [MiscSetting(9)]
        public byte DisappearLinkID { get; set; }
    }
}
