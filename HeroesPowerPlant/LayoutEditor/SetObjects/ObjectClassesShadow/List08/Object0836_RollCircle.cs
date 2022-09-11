using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0836_RollCircle : SetObjectShadow
    {
        public enum EModel : int
        {
            Big,
            Small,
            Head
        }

        //Tornado Flying Objects
        //RollCircle 
        [MiscSetting]
        public EModel Model { get; set; }
        [MiscSetting, Description("Number of objects orbiting")]
        public int NumberOfObjects { get; set; }
        [MiscSetting, Description("m")]
        public float Radius { get; set; }
        [MiscSetting, Description("deg/sec")]
        public float CircleSpeed { get; set; }
        [MiscSetting, Description("deg/sec")]
        public float ObjAngSpd_X { get; set; }
        [MiscSetting, Description("deg/sec")]
        public float ObjAngSpd_Y { get; set; }
        [MiscSetting, Description("deg/sec")]
        public float ObjAngSpd_Z { get; set; }
    }
}
