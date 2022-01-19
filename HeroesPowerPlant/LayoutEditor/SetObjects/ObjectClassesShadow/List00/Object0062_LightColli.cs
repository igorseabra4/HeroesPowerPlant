using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0062_LightColli : SetObjectShadow {
        //LightColli [LightColliMasterTask parent]
        //Enums:
        // SWITCH_ON, SWITCH_OFF
        // CYLINDER, SPHERE (Box is implicit from Area(), Cylinder is unused but functional)
        // LightNo:PLAYER_0-3; OBJECT_0-3; ENEMY_0-3, KAGE, OTHER_0-2, DoNotUse
        // Light OFF, Light ON
        // Effect OFF, Effect ON
        // Params:
        // LightColli(LightFlag, LightNumber, EffectFlag, EffectNumber)

        public override void CreateTransformMatrix() {
            switch (RangeShape) {
                case LightColli_RangeShape.Box:
                    transformMatrix = Matrix.Scaling(RangeX * 2, RangeY * 2, RangeZ * 2);
                    break;
                case LightColli_RangeShape.Sphere:
                    transformMatrix = Matrix.Scaling(RangeX * 2);
                    break;
                case LightColli_RangeShape.Cylinder:
                    // LightColli Cylinder variant is X,Z,X, RotX(90)
                    transformMatrix = Matrix.Scaling(RangeX * 2, RangeZ * 2, RangeX * 2);
                    transformMatrix *= Matrix.RotationX(90 * (MathUtil.Pi / 180));
                    break;
            }

            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public override void Draw(SharpRenderer renderer) {
            if (RangeShape == LightColli_RangeShape.Box)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (RangeShape == LightColli_RangeShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (RangeShape == LightColli_RangeShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
        }

        public LightColli_SwitchMode SwitchMode {
            get => (LightColli_SwitchMode)ReadInt(0);
            set => Write(0, (int)value);
        }

        public LightColli_RangeShape RangeShape {
            get => (LightColli_RangeShape)ReadInt(4);
            set => Write(4, (int)value);
        }
        public CommonNoYes LightingIsEnabled {
            get => (CommonNoYes)ReadInt(8);
            set => Write(8, (int)value);
        }

        [Description("Presets from the light.bin in the stage folder")]
        public LightColli_LightNumber LightNumber {
            get => (LightColli_LightNumber)ReadInt(12);
            set => Write(12, (int)value);
        }

        public CommonNoYes EffectIsEmitting {
            get => (CommonNoYes)ReadInt(16);
            set => Write(16, (int)value);
        }

        public int EffectNumber {
            get => ReadInt(20);
            set => Write(20, value);
        }

        public float RangeX {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float RangeY {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public float RangeZ {
            get => ReadFloat(32);
            set => Write(32, value);
        }
    }

    public enum LightColli_SwitchMode {
        WhileInRadius,
        ToggleOn,
        ToggleOff
    }

    public enum LightColli_RangeShape {
        Box,
        Sphere,
        Cylinder
    }

    public enum LightColli_LightNumber {
        // LightNo:PLAYER_0-3; OBJECT_0-3; ENEMY_0-3, KAGE, OTHER_0-2, DoNotUse
        Player_0,
        Player_1,
        Player_2,
        Player_3,
        Object_0,
        Object_1,
        Object_2,
        Object_3,
        Enemy_0,
        Enemy_1,
        Enemy_2,
        Enemy_3,
        ShadowsOrKage,
        Other_0,
        Other_1,
        Other_2,
        DoNotUse
    }
}
