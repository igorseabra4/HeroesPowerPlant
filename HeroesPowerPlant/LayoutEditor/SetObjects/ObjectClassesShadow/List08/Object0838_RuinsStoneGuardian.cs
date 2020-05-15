namespace HeroesPowerPlant.LayoutEditor {
    public class Object0838_RuinsStoneGuardian : SetObjectShadow {
        //StoneStatue(Active{off,on},Mirror{off,on},Range_Radius m, Range_height m)
        public StoneStatueOffOn Active {
            get => (StoneStatueOffOn)ReadInt(0);
            set => Write(0, (int)value);
        }
        public StoneStatueOffOn Mirror {
            get => (StoneStatueOffOn)ReadInt(4);
            set => Write(4, (int)value);
        }
        public float DetectRadius {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float DetectHeight {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }

    public enum StoneStatueOffOn {
        Off,
        On
    }
}
