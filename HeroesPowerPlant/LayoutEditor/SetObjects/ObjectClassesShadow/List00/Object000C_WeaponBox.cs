namespace HeroesPowerPlant.LayoutEditor {
    public class Object000C_WeaponBox : SetObjectShadow {
        public BoxType BoxType {
            get => (BoxType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public Weapon Weapon {
            get => (Weapon)ReadInt(4);
            set => Write(4, (int)value);
        }
    }
}
