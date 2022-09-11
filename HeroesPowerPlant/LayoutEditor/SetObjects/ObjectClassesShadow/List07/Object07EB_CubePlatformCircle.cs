using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07EB_CubePlatformCircle : SetObjectShadow
    {
        [MiscSetting, Description("This objects spawns this many cubes that orbit around Radius at CircleSpeed")]
        public int NumberOfCubes { get; set; }

        [MiscSetting, Description("m")]
        public float Radius { get; set; }

        [MiscSetting, Description("deg/sec")]
        public float CircleSpeed { get; set; }
    }
}
