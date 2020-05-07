using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0034_Fire : SetObjectShadow {
        // Fire(ScaleX, ScaleY, ScaleZ)
        // Probably late in development changed to hardcoded fire scale
        // OR oversight and forgot to read from params
        // Still documenting this as some objects have misc bytes, even if unused
        // In the future a Gecko code might re-enable the object to read these.

        [Description("These fields are unused. A gecko code may make them usable.")]
        public float Fire_ScaleX {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Fire_ScaleY {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Fire_ScaleZ {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
