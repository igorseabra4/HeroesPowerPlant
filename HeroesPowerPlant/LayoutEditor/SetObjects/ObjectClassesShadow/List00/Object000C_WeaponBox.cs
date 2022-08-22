using SharpDX;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000C_WeaponBox : SetObjectShadow
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

        public EBoxType BoxType { get; set; }
        public EWeapon Weapon { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            BoxType = (EBoxType)reader.ReadInt32();
            Weapon = (EWeapon)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)BoxType);
            writer.Write((int)Weapon);
        }
    }
}
