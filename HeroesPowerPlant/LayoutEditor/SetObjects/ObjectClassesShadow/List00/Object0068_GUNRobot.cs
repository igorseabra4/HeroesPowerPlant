namespace HeroesPowerPlant.LayoutEditor {
    public class Object0068_GUNRobot : SetObjectShadow {
        
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

        public GUNRobotAppearType AppearType { //7
            get => (GUNRobotAppearType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public GUNRobotWeaponType WeaponType { //8
            get => (GUNRobotWeaponType)ReadInt(32);
            set => Write(32, (int)value);
        }

        public GUNRobotBodyType BodyType { //9
            get => (GUNRobotBodyType)ReadInt(36);
            set => Write(36, (int)value);
        }

        public float OffsetPos_X { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }

        public float OffsetPos_Y { //11
            get => ReadFloat(44);
            set => Write(44, value);
        }

        public float OffsetPos_Z { //12
            get => ReadFloat(48);
            set => Write(48, value);
        }

        public CommonWaitActMoveType WaitActMoveType { //13
            get => (CommonWaitActMoveType)ReadInt(52);
            set => Write(52, (int)value);
        }

        public float Pos0_ActionTime { //14
            get => ReadFloat(56);
            set => Write(56, value);
        }

        public CommonActionType Pos0_ActionType { //15
            get => (CommonActionType)ReadInt(60);
            set => Write(60, (int)value);
        }

        public float Pos0_MoveSpeedRatio { //16
            get => ReadFloat(64);
            set => Write(64, value);
        }

        public float Pos0_TranslationXFromOrigin { //17
            get => ReadFloat(68);
            set => Write(68, value);
        }

        public float Pos0_TranslationZFromOrigin { //18
            get => ReadFloat(72);
            set => Write(72, value);
        }

        public float Pos1_ActionTime { //19
            get => ReadFloat(76);
            set => Write(76, value);
        }

        public CommonActionType Pos1_ActionType { //20
            get => (CommonActionType)ReadInt(80);
            set => Write(80, (int)value);
        }

        public float Pos1_MoveSpeedRatio { //21
            get => ReadFloat(84);
            set => Write(84, value);
        }

        public float Pos1_TranslationXFromOrigin { //22
            get => ReadFloat(88);
            set => Write(88, value);
        }

        public float Pos1_TranslationZFromOrigin { //23
            get => ReadFloat(92);
            set => Write(92, value);
        }

        public float Pos2_ActionTime { //24
            get => ReadFloat(96);
            set => Write(96, value);
        }

        public CommonActionType Pos2_ActionType { //25
            get => (CommonActionType)ReadInt(100);
            set => Write(100, (int)value);
        }

        public float Pos2_MoveSpeedRatio { //26
            get => ReadFloat(104);
            set => Write(104, value);
        }


    }

    public enum GUNRobotBodyType {
        Trooper,
        GigaTrooper
    }

    public enum GUNRobotAppearType {
        WAIT_ACT,
        OFFSET,
        WARP,
        XXX
    }

    public enum GUNRobotWeaponType {
        AUTORIFLE,
        AIRCRAFTRIFLE,
        BAZOOKA,
        ROCKET4,
        ROCKET8,
        LASERRIFLE
    }
}
