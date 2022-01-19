using SharpDX;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object003A_SpecialWeaponBox : SetObjectShadow {

        public override void CreateTransformMatrix()
        {
            // function 800c9ed4 | RotationTemplateGen
            var shift = MathUtil.Pi / 180f;
            transformMatrix =
                Matrix.RotationZ(Rotation.Z * shift) *
                Matrix.RotationX(Rotation.X * shift) *
                Matrix.RotationY(Rotation.Y * shift) *
                Matrix.Translation(Position.X, Position.Y + 10f, Position.Z);
            CreateBoundingBox();
        }

        public Weapon WeaponIfSpecialWeaponsNotUnlocked {
            get => (Weapon)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}

