using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1137_StretchGrass : SetObjectShadow
    {
        //SetStretchGrass(level, angle, length, charge, NOBI_KUSA)
        //HPP name: Slingshot Plant
        [MiscSetting]
        public float Strength { get; set; }
        [MiscSetting]
        public float Angle { get; set; }
        [MiscSetting]
        public float NoControlTimeAfterLaunch { get; set; }
        [MiscSetting, Description("How far back the sling stretches")]
        public float Length { get; set; }
        [MiscSetting, Description("Time to wait after fully pulled back Length amount before launch")]
        public float Charge { get; set; }
    }
}
