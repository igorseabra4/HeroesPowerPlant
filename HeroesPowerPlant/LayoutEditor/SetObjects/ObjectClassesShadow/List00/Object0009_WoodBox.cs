using SharpDX;
using System.ComponentModel;

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

        [MiscSetting]
        public EBoxType BoxType { get; set; }
        [MiscSetting]
        public EBoxItem BoxItem { get; set; }
        [MiscSetting, Description("Use this if ItemType is any other type")]
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
