using Collada141;
using SharpDX;
using System.IO;

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

        public EWeapon WeaponIfSpecialWeaponsNotUnlocked
        {
            get => (EWeapon)ReadInt(0);
            set => Write(0, (int)value);
        }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            WeaponIfSpecialWeaponsNotUnlocked = (EWeapon)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)WeaponIfSpecialWeaponsNotUnlocked);
        }
    }
}

