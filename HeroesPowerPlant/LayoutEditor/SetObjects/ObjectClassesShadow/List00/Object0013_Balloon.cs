namespace HeroesPowerPlant.LayoutEditor {
    public class Object0013_Balloon : SetObjectShadow {
        public BalloonType BalloonType  {
            get => (BalloonType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public ItemShadow ItemType {
            get => (ItemShadow)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float SpeedDampAmount {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float OrbitDistance {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float TranslationX {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float TranslationY {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float TranslationZ {
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }

    public enum BalloonType {
        TranslationOnceAndDisappear,
        TranslationLoop,
        Orbit
    }
}
