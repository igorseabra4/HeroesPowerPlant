namespace HeroesPowerPlant.LayoutEditor {
    public class Object07D2_ColorSwitch : SetObjectShadow {
        public int type0 { //0 or 1 or 2 or 3
            get => ReadInt(0);
            set => Write(0, value);
        }
        public int type1 { //0 or 1
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float f2 {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float f3 {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        public float f4 {
            get => ReadFloat(16);
            set => Write(16, value);
        }
        public float f5 {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}

