namespace HeroesPowerPlant.LayoutEditor {
    public class Object000B_UnbreakableBox : SetObjectShadow {
        public BoxType BoxType {
            get => (BoxType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}
