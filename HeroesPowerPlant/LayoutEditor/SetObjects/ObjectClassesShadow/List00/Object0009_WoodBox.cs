using SharpDX;
using System.ComponentModel;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0009_WoodBox : SetObjectShadow
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

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            BoxType = (EBoxType)reader.ReadInt32();
            BoxItem = (EBoxItem)reader.ReadInt32();
            ItemTypeModifier = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)BoxType);
            writer.Write((int)BoxItem);
            writer.Write(ItemTypeModifier);
        }

        public EBoxType BoxType { get; set; }
        public EBoxItem BoxItem { get; set; }
        [Description("Use this if ItemType is any other type")]
        public int ItemTypeModifier { get; set; }

        [Description("Use this if ItemType is ItemCapsule")]
        public EShadowItem ModifierCapsule
        {
            get => (EShadowItem)ItemTypeModifier;
            set => ItemTypeModifier = (int)value;
        }

        [Description("Use this if ItemType is Weapon")]
        public EWeapon ModifierWeapon
        {
            get => (EWeapon)ItemTypeModifier;
            set => ItemTypeModifier = (int)value;
        }

        [Description("Use this if ItemType is EnergyCore")]
        public EEnergyCoreType ModifierEnergyCore
        {
            get => (EEnergyCoreType)ItemTypeModifier;
            set => ItemTypeModifier = (int)value;
        }
    }
}
