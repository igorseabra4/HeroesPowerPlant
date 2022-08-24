using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectHeroes
    {
        [MiscSetting, Description("Defaults to 5.0")]
        public float Speed { get; set; }
        [MiscSetting, Description("In frames")]
        public short NoControlTime { get; set; }
        [MiscSetting(padAfter: 8), Description("Usually 0")]
        public float Unknown1 { get; set; }
        [MiscSetting, Description("Usually 0")]
        public float Unknown2 { get; set; }
    }
}