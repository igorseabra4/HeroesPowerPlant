namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0065_GUNBeetle : SetObjectShadow
    {

        // EnemyBase
        public float MoveRange
        { //0
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float SearchRange
        { //1
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float SearchAngle
        { //2
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SearchWidth
        { //3
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SearchHeight
        { //4
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float SearchHeightOffset
        { //5
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float MoveSpeedRatio
        { //6
            get => ReadFloat(24);
            set => Write(24, value);
        }
        // end EnemyBase

        public GUNBeetleAppearType AppearType
        { //7
            get => (GUNBeetleAppearType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public GUNBeetleActionType ActionType
        { //8
            get => (GUNBeetleActionType)ReadInt(32);
            set => Write(32, (int)value);
        }

        public GUNBeetlePathType PathType
        { //9
            get => (GUNBeetlePathType)ReadInt(36);
            set => Write(36, (int)value);
        }

        public float PathVariable
        { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }

        public float AttackStart
        { //11
            get => ReadFloat(44);
            set => Write(44, value);
        }
        public float AttackEnd
        { //12
            get => ReadFloat(48);
            set => Write(48, value);
        }

        public CommonNoYes PatrolReversed
        { //13
            get => (CommonNoYes)ReadInt(52);
            set => Write(52, (int)value);
        }

        public CommonNoYes IsGolden
        { //14
            get => (CommonNoYes)ReadInt(56);
            set => Write(56, (int)value);
        }
        public GUNBeetleWeaponType WeaponType
        { //15
            get => (GUNBeetleWeaponType)ReadInt(60);
            set => Write(60, (int)value);
        }

        public float SparkDischarge
        { //16
            get => ReadFloat(64);
            set => Write(64, value);
        }
        public float SparkWait
        { //17
            get => ReadFloat(68);
            set => Write(68, value);
        }
    }

    public enum GUNBeetleAppearType
    {
        WAIT_FLOATING,
        MOVE_ON_PATH
    }

    public enum GUNBeetleActionType
    {
        NONE,
        SHOCK,
        USE_WEAPON,
    }
    public enum GUNBeetleWeaponType
    {
        RIFLE,
        ROCKET4,
        BOMB,
        MACHINEGUN,
        NONE
    }

    public enum GUNBeetlePathType
    {
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
}
