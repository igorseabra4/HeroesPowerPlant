namespace HeroesPowerPlant.LayoutEditor {
    public class Object1131_Vine : SetObjectShadow {
        public float AttackRange {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float AttackSec {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ReviveSec {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
