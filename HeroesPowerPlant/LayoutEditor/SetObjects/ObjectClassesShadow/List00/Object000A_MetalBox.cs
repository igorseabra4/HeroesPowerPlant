using SharpDX;
using System.ComponentModel;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000A_MetalBox : SetObjectShadow
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
            BoxItem = (count > 4) ? (EBoxItem)reader.ReadInt32() : EBoxItem.NotValidInObject;
            ItemTypeModifier = (count > 8) ? reader.ReadInt32() : -1;
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)BoxType);
            if (BoxItem != EBoxItem.NotValidInObject)
            {
                writer.Write((int)BoxItem);

                if (ItemTypeModifier != -1)
                    writer.Write(ItemTypeModifier);
            }
        }

        public EBoxType BoxType { get; set; }

        public static string Warning => "If you see \"NotValidInObject\" or -1, do not edit field.";

        public EBoxItem BoxItem { get; set; }

        [Description("Use this if ItemType is any other type")]
        public int ItemTypeModifier { get; set; }

        [Description("Use this if ItemType is ItemCapsule")]
        public EItemShadow ModifierCapsule
        {
            get => (EItemShadow)ItemTypeModifier;
            set => ItemTypeModifier = (int)value;
        }

        [Description("Use this if ItemType is Weapon")]
        public EWeapon ModifierWeapon
        {
            get => (EWeapon)ItemTypeModifier;
            set => ItemTypeModifier = (int)value;
        }

        [Description("Use this if ItemType is EnergyCore")]
        public EEnergyCore ModifierEnergyCore
        {
            get => (EEnergyCore)ItemTypeModifier;
            set => ItemTypeModifier = (int)value;
        }
    }
}
