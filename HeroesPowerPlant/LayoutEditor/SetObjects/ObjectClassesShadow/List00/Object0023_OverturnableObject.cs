namespace HeroesPowerPlant.LayoutEditor {
    public class Object0023_OverturnableObject : SetObjectShadow {
        public int ModelIfMultiple {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public int UnusedInt {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}
