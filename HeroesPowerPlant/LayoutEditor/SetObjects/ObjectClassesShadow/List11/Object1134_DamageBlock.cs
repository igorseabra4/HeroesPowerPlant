namespace HeroesPowerPlant.LayoutEditor {
    public class Object1134_DamageBlock : SetObjectShadow {
        //AKA Green Spike Ball
        //DamageBlock(kind)
        public int Kind {
            get => ReadInt(0);
            set => Write(0, value);
        }
    }
}
