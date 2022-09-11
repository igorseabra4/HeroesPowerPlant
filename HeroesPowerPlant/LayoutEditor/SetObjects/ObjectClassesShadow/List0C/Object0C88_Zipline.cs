using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C88_Zipline : SetObjectShadow
    {
        //Coaster(PATH_NO, MOVE_SPPED m/sec, LAUNCH_SPD m/sec, LAUNCH_ANG deg, LAUNCH_NO_CTRL_TIME sec)

        [MiscSetting, Description("PATH/SplineID to follow when the player grabs object")]
        public int SplineID { get; set; }

        [MiscSetting, Description("m/sec")]
        public float MoveSpeed { get; set; }

        [MiscSetting, Description("m/sec")]
        public float LaunchSpeed { get; set; }

        [MiscSetting, Description("deg")]
        public float LaunchAngle { get; set; }

        [MiscSetting, Description("sec")]
        public float LaunchNoControlTime { get; set; }
    }
}
