using SharpDX;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object003A_SpecialWeaponBox : SetObjectShadow {

        public override void CreateTransformMatrix()
        {
            transformMatrix =
                Matrix.Translation(Position.X, Position.Y + 10f, Position.Z);

            CreateBoundingBox();
        }

        public Weapon WeaponIfSpecialWeaponsNotUnlocked {
            get => (Weapon)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}

