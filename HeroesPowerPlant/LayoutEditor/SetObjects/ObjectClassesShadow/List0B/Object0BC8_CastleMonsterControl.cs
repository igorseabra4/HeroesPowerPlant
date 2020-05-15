namespace HeroesPowerPlant.LayoutEditor {
    public class Object0BC8_CastleMonsterControl : SetObjectShadow {
        //MonsterControl
        public MonsterStateEnum MonsterState {
            get => (MonsterStateEnum)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float X_Length {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Y_Length {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float Z_Length {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }

    public enum MonsterStateEnum {
        Start,
        Miss,
        End
    }
}
