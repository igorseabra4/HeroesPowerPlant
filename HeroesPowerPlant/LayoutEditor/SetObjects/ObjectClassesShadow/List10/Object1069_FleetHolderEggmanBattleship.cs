namespace HeroesPowerPlant.LayoutEditor {
    public class Object1069_FleetHolderEggmanBattleship : SetObjectShadow {

        public EggFleetType ShipType {
            get => (EggFleetType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float RangeRadius {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float RangeHeight {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float MoveLength {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        public float MoveSpeed {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float FireTiming {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }

    public enum EggFleetType {
        BATTLE,
        MOTHER
    }
}

