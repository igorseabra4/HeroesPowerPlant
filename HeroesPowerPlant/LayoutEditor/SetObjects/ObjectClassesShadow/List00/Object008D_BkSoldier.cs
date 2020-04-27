using System.Linq;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object008D_BkSoldier : SetObjectShadow {

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

        public CommonNoYes HaveShield { //7
            get => (CommonNoYes)ReadInt(28);
            set => Write(28, (int)value);
        }

        public BkSoldierWeaponType WeaponType { //8
            get => (BkSoldierWeaponType)ReadInt(32);
            set => Write(32, (int)value);
        }

        public BkSoldierAppearType AppearType { //9
            get => (BkSoldierAppearType)ReadInt(36);
            set => Write(36, (int)value);
        }

        public BkSoldierWaitType Pos0_WaitType { //10
            get => (BkSoldierWaitType)ReadInt(40);
            set => Write(40, (int)value);
        }

        public float Pos0_WaitSec { //11
            get => ReadFloat(44);
            set => Write(44, value);
        }

        public float Pos0_MoveSpeedRatio { //12
            get => ReadFloat(48);
            set => Write(48, value);
        }

        public float Pos0_TranslationXFromOrigin { //13
            get => ReadFloat(52);
            set => Write(52, value);
        }

        public float Pos0_TranslationZFromOrigin { //14
            get => ReadFloat(56);
            set => Write(56, value);
        }

        public BkSoldierWaitType Pos1_WaitType { //15
            get => (BkSoldierWaitType)ReadInt(60);
            set => Write(60, (int)value);
        }

        public float Pos1_WaitSec { //16
            get => ReadFloat(64);
            set => Write(64, value);
        }

        public float Pos1_MoveSpeedRatio { //17
            get => ReadFloat(68);
            set => Write(68, value);
        }

        public float Pos1_TranslationXFromOrigin { //18
            get => ReadFloat(72);
            set => Write(72, value);
        }

        public float Pos1_TranslationZFromOrigin { //19
            get => ReadFloat(76);
            set => Write(76, value);
        }

        public BkSoldierWaitType Pos2_WaitType { //20
            get => (BkSoldierWaitType)ReadInt(80);
            set => Write(80, (int)value);
        }

        public float Pos2_WaitSec { //21
            get => ReadFloat(84);
            set => Write(84, value);
        }

        public float Pos2_MoveSpeedRatio { //22
            get => ReadFloat(88);
            set => Write(88, value);
        }

        public BkNoYes IsOnAirSaucer { //23 
            // there are two BkSoldier objects somewhere in the game layouts
            // that only have 92 bytes instead of 96, this accounts for that
            get {
                if (MiscSettings.Length > 92)
                    return (BkNoYes)ReadInt(92);
                return (BkNoYes)2;
            } 
            set {
                if (MiscSettings.Length < 96) {
                    return;
                }
                Write(92, (int)value);
            }
        }

    }

    public enum BkSoldierWeaponType {
        NONE,
        BLACK_SWORD,
        LIGHT_SHOT,
        FLASH_SHOT,
        BLACK_BARREL,
        SPLITTER,
        VACUUM_POD,
        HEAVY_SHOT,
        RING_SHOT
    }
    public enum BkSoldierAppearType {
        STAND,
        LINEAR_MOVE,
        TRIANGLE_MOVE,
        RANDOM_MOVE,
        OFFSETPOS,
        WARP
    }
    public enum BkSoldierWaitType {
        ATTACK,
        HIDE,
        KAMAE
    }

    public enum BkNoYes {
        NO,
        YES,
        DO_NOT_CHANGE_ME_IF_THIS_VALUE_IS_ON_ORIGINAL_OBJECT
    }
}
