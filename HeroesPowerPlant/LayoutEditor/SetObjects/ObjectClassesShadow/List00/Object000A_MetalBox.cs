using SharpDX;
using System.ComponentModel;

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

        public EBoxType BoxType
        {
            get => (EBoxType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public string Warning => "If you see \"NotValidInObject\" or -1, Do not edit field.";

        public EBoxItem ItemType
        {
            get
            {
                if (MiscSettings.Length > 4)
                    return (EBoxItem)ReadInt(4);
                return (EBoxItem)(-1);
            }
            set
            {
                if (MiscSettings.Length < 8)
                    return;
                Write(4, (int)value);
            }
        }

        [Description("Use this if ItemType is any other type")]
        public int ItemTypeModifier
        {
            get
            {
                if (MiscSettings.Length > 8)
                    return ReadInt(8);
                return -1;
            }
            set
            {
                if (MiscSettings.Length < 12)
                    return;
                Write(8, value);
            }
        }

        [Description("Use this if ItemType is ItemCapsule")]
        public EShadowItem ModifierCapsule
        {
            get
            {
                if (MiscSettings.Length > 8)
                    return (EShadowItem)ReadInt(8);
                return (EShadowItem)(-1);
            }
            set
            {
                if (MiscSettings.Length < 12)
                    return;
                Write(8, (int)value);
            }
        }

        [Description("Use this if ItemType is Weapon")]
        public EWeapon ModifierWeapon
        {
            get
            {
                if (MiscSettings.Length > 8)
                    return (EWeapon)ReadInt(8);
                return (EWeapon)(-1);
            }
            set
            {
                if (MiscSettings.Length < 12)
                    return;
                Write(8, (int)value);
            }
        }

        [Description("Use this if ItemType is EnergyCore")]
        public EEnergyCoreType ModifierEnergyCore
        {
            get
            {
                if (MiscSettings.Length > 8)
                    return (EEnergyCoreType)ReadInt(8);
                return (EEnergyCoreType)(-1);
            }
            set
            {
                if (MiscSettings.Length < 12)
                    return;
                Write(8, (int)value);
            }
        }
    }
}
