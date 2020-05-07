namespace HeroesPowerPlant.LayoutEditor {
    public class Object003A_SpecialWeaponBox : SetObjectShadow {
        public Weapon WeaponIfSpecialWeaponsNotUnlocked {
            get => (Weapon)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}

