using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002C_RollDoor : SetObjectHeroes
    {
        [MiscSetting, Description("Defaults to 5.0")]
        public float Power { get; set; }
        [MiscSetting, Description("In degrees")]
        public float Elevation { get; set; }
        [MiscSetting, Description("In frames")]
        public short NoControlTime { get; set; }
    }
}