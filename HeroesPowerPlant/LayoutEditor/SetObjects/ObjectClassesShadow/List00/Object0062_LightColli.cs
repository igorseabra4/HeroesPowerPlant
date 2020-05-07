using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0062_LightColli : SetObjectShadow {
        //LightColli [LightColliMasterTask parent]
        //Enums:
        // SWITCH_ON, SWITCH_OFF
        // CYLINDER
        // LightNo:PLAYER_0-3; OBJECT_0-3; ENEMY_0-3, KAGE, OTHER_0-2, DoNotUse
        // Light OFF, Light ON
        // Effect OFF, Effect ON
        // Params:
        // LightColli(LightFlag, LightNumber, EffectFlag, EffectNumber)
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
        Cylinder,
        Capsule,
        Box,
        Sphere
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
