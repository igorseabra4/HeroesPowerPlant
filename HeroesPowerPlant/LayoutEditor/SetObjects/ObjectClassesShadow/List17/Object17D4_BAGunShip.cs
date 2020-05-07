namespace HeroesPowerPlant.LayoutEditor {
    public class Object11D4_BAGunShip : SetObjectShadow {

        public float DetectRangeXZ {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float DetectRangeY {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float TimeUntilTakeOff {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public BAGunShipAttack Attack {
            get => (BAGunShipAttack)ReadInt(12);
            set => Write(12, (int)value);
        }

        public int int4 {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public int int5 {
            get => ReadInt(20);
            set => Write(20, value);
        }

        public int int6 {
            get => ReadInt(24);
            set => Write(24, value);
        }

        public int int7 {
            get => ReadInt(28);
            set => Write(28, value);
        }

        public float AttackInterval {
            get => ReadFloat(32);
            set => Write(32, value);
        }
    }

    public enum BAGunShipAttack {
        NONE,
        ATTACK,
        ATTACK2=5
    }
}

