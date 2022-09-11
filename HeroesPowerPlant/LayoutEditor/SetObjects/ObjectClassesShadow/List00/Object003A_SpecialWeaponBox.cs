using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object003A_SpecialWeaponBox : SetObjectShadow
    {
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

        [MiscSetting, Description("Weapon to use if special weapons not unlocked")]
        public EWeapon Weapon { get; set; }
    }
}

