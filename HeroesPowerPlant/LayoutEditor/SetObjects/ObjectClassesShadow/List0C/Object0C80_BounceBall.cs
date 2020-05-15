namespace HeroesPowerPlant.LayoutEditor {
    public class Object0C80_BounceBall : SetObjectShadow {
        //CircusBall [Horror/Circus skins] (type, level, nocontrol time (sec), angle)
        public BounceBallAppearType AppearType {
            get => (BounceBallAppearType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float Strength {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float NoControlTime {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float Angle {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }

    public enum BounceBallAppearType {
        Always,
        AfterLinkIDCleared
    }
}
