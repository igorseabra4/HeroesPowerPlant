namespace HeroesPowerPlant.LayoutEditor {
    public class Object07DA_MatrixTerminal1 : SetObjectShadow {

        public float f0 {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float f1 {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float f2 {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
