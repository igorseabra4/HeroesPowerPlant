using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object14BE_ArkGreenLaser : SetObjectShadow
    {
        //ArkLaser

        [MiscSetting, Description("Y-axis increase up/down half of this value, split equally")]
        public float Length { get; set; }
        [MiscSetting, Description("How long the laser runs (seconds) once active")]
        public float ActiveTime { get; set; }
        [MiscSetting, Description("How long the laser waits (seconds) before firing again")]
        public float RechargeTime { get; set; }
    }
}
