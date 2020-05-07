using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object001A_Wind : SetObjectShadow {
        // Technically a copy of "Fan", with a unique model

        public FanType DirectionType { //0 or 1
            get => (FanType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public FanForm BlowerType { //0 or 1
            get => (FanForm)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Radius {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Only for BlowerType BoxType")]
        public float BoxTypeAirHeight { //always 0
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Cylinder Type Air Height; Box Type Radius")]
        public float AirHeightANDBoxTypeRadius {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float AirStrength {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float TimeToRun {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float TimeToRecharge {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public CommonNoYes HasModel { //0 or 1
            get => (CommonNoYes)ReadInt(32);
            set => Write(32, (int)value);
        }
        public FanRunning WindBlowing { //-1 or 255
            get => (FanRunning)ReadInt(36);
            set => Write(36, (int)value);
        }

        [Description("WindBlowing shares this, can set to LinkID to watch for")]
        public int LinkIDMakeRun {
            get => ReadInt(36);
            set => Write(36, value);
        }
    }
}

