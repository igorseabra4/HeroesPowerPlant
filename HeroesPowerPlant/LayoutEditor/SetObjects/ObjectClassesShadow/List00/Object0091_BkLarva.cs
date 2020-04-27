namespace HeroesPowerPlant.LayoutEditor {
    public class Object0091_BkLarva : SetObjectShadow {

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

        public int NumberOfLarva { //7
            get => ReadInt(28);
            set => Write(28, value);
        }

        public float AppearRange { //8
            get => ReadFloat(32);
            set => Write(32, value);
        }

        public BkLarvaAppearType AppearType { //9
            get => (BkLarvaAppearType)ReadInt(36);
            set => Write(36, (int)value);
        }

        public float Offset_Y { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }

    }

    public enum BkLarvaAppearType {
        NORMAL,
        DROP
    }
}
