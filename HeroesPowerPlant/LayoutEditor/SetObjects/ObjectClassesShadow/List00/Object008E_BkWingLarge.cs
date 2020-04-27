namespace HeroesPowerPlant.LayoutEditor {
    public class Object008E_BkWingLarge : SetObjectShadow {

        // EnemyBase
        public float MoveRange { //0
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float SearchRange { //1
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float SearchAngle { //2
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SearchWidth { //3
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SearchHeight { //4
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float SearchHeightOffset { //5
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float MoveSpeedRatio { //6
            get => ReadFloat(24);
            set => Write(24, value);
        }
        // end EnemyBase

        public BkWingLargeAppearType AppearType { //7
            get => (BkWingLargeAppearType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public BkWingLargeActionType ActionType { //8
            get => (BkWingLargeActionType)ReadInt(32);
            set => Write(32, (int)value);
        }

        public BkWingLargePathType PathType { //9
            get => (BkWingLargePathType)ReadInt(36);
            set => Write(36, (int)value);
        }

        public float PathVariable { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }

        public float AttackStart { //11
            get => ReadFloat(44);
            set => Write(44, value);
        }
        public float AttackEnd { //12
            get => ReadFloat(48);
            set => Write(48, value);
        }

        public CommonNoYes PatrolReversed { //13
            get => (CommonNoYes)ReadInt(52);
            set => Write(52, (int)value);
        }

        public BkWingLargeType BodyAndDeathType { //14
            get => (BkWingLargeType)ReadInt(56);
            set => Write(56, (int)value);
        }
    }

    public enum BkWingLargeAppearType {
        WAIT_FLOATING,
        MOVE_ON_PATH
    }

    public enum BkWingLargeActionType {
        NONE,
        AIR_CUTTER
    }

    public enum BkWingLargePathType {
        LEFTRIGHT,
        UPDOWN,
        RIGHTLEFT,
        DOWNUP,
        FLY_FORWARD_UPDOWN,
        FLY_LEFT,
        FLY_FORWARD,
        FLY_FORWARD_SWOOP,
        CIRCLE
    }

    public enum BkWingLargeType {
        BLACK_HAWK_DISAPPEAR_ON_KILL=0,
        BLACK_HAWK_FALL_ON_KILL=1,
        BLACK_VOLT_DISAPPEAR_ON_KILL=16,
        BLACK_VOLT_FALL_ON_KILL=17
    }
}
