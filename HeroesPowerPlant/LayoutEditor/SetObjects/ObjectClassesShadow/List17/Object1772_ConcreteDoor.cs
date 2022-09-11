using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1772_ConcreteDoor : SetObjectShadow
    {
        [MiscSetting]
        public float Detect_X { get; set; }
        [MiscSetting]
        public float Detect_Y { get; set; }
        [MiscSetting]
        public float Detect_Z { get; set; }
        [MiscSetting, Description("m/s to close\n smaller = slower")]
        public float CloseSpeed { get; set; }
    }
}
