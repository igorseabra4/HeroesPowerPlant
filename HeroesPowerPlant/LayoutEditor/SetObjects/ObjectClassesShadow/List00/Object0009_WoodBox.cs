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

        public BoxType BoxType
        {
            get => (BoxType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public BoxItem ItemType
        {
            get => (BoxItem)ReadInt(4);
            set => Write(4, (int)value);
        }

        [Description("Use this if ItemType is any other type")]
        public int ItemTypeModifier
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        [Description("Use this if ItemType is ItemCapsule")]
        public ItemShadow ModifierCapsule
        {
            get => (ItemShadow)ReadInt(8);
            set => Write(8, (int)value);
        }

        [Description("Use this if ItemType is Weapon")]
        public Weapon ModifierWeapon
        {
            get => (Weapon)ReadInt(8);
            set => Write(8, (int)value);
        }

        [Description("Use this if ItemType is EnergyCore")]
        public EnergyCoreType ModifierEnergyCore
        {
            get => (EnergyCoreType)ReadInt(8);
            set => Write(8, (int)value);
        }
    }
}
