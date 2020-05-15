namespace HeroesPowerPlant.LayoutEditor {
    public class Object13F0_SlideoutPlatform : SetObjectShadow {
        //SetPopFooting(model sec_begin, sec_wait)
        public int Model {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public float SecBegin {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float SecWait {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
