namespace HeroesPowerPlant.LayoutEditor {
    public class Object007A_EggShadowAndroid : SetObjectShadow {
        
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

        public EggShadowAndroidAppearType AppearType { //7
            get => (EggShadowAndroidAppearType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public float OffsetPos_X { //8
            get => ReadFloat(32);
            set => Write(32, value);
        }

        public float OffsetPos_Y { //9
            get => ReadFloat(36);
            set => Write(36, value);
        }

        public float OffsetPos_Z { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }
    }

    public enum EggShadowAndroidAppearType {
        STAND,
        OFFSET
    }
}
