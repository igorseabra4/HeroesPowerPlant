namespace HeroesPowerPlant.LayoutEditor {
    public class Object07D4_ElecSecurity : SetObjectShadow {

        public ElecSecurityType Type {
            get => (ElecSecurityType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float DetectionRange {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public enum ElecSecurityType {
            STRONG,
            MEDIUM,
            WEAK,
            TIKAL,
        }
    }
}
