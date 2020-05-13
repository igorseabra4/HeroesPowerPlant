using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object138B_MeteorsHolder : SetObjectShadow {
        //MeteorsHolder(MOVE_SPD m/s, DIRECTION_X deg, DIRECTION_Y deg, ROT_SPD_X deg/s, ROT_SPD_Z deg/s, ROT_SPD_Y deg/s, LIFETIME s, METEOR_S)

        [Description("m")]
        public float MaxTravelDistance {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        [Description("m/s")]
        public float MoveSpeed {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("deg")]
        public float Direction_X {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("deg")]
        public float Direction_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("deg/s")]
        public float RotSpd_X {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("deg/s")]
        public float RotSpd_Z {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("deg/s")]
        public float RotSpd_Y {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        [Description("s\n Time until meteor explodes")]
        public float Lifetime {
            get => ReadFloat(28);
            set => Write(28, value);
        }
    }
}
