using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0C88_Zipline : SetObjectShadow {
        //Coaster(PATH_NO, MOVE_SPPED m/sec, LAUNCH_SPD m/sec, LAUNCH_ANG deg, LAUNCH_NO_CTRL_TIME sec)
        
        [Description("PATH/SplineID to follow when the player grabs object")]
        public int SplineID {
            get => ReadInt(0);
            set => Write(0, value);
        }

        [Description("m/sec")]
        public float MoveSpeed {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("m/sec")]
        public float LaunchSpeed {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("deg")]
        public float LaunchAngle {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("sec")]
        public float LaunchNoControlTime {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}
