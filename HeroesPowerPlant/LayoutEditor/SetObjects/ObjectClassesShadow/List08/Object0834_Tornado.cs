using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0834_Tornado : SetObjectShadow
    {
        public enum ESpinDirection : int
        {
            Left,
            Right
        }

        //Tornado
        [MiscSetting]
        public ESpinDirection SpinDirection { get; set; }
        [MiscSetting, Description("m/s")]
        public float SpinningSpeed { get; set; }
        [MiscSetting, Description("m/s")]
        public float UpSpeed { get; set; }
        [MiscSetting, Description("m")]
        public float Cylinder_Radius { get; set; }
        [MiscSetting, Description("m")]
        public float Cylinder_Height { get; set; }
        [MiscSetting, Description("m")]
        public float Cylinder_Offset_Y { get; set; }
        [MiscSetting, Description("deg")]
        public float Launch_X { get; set; }
        [MiscSetting, Description("deg")]
        public float Launch_Y { get; set; }
        [MiscSetting, Description("m/s")]
        public float Launch_Speed { get; set; }
        [MiscSetting, Description("sec")]
        public float NoControlSec { get; set; }
    }
}
