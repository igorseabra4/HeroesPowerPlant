using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object003X_UnusedMiscByteScaleType : SetObjectShadow {
        // Fire(ScaleX, ScaleY, ScaleZ)
        // Probably late in development changed to hardcoded fire scale (applies to multiple objects)
        // OR oversight and forgot to read from params
        // Still documenting this as some objects have misc bytes, even if unused
        // In the future a Gecko code might re-enable the object to read these.

        [Description("These fields are unused. A gecko code may make them usable.")]
        public float ScaleX {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float ScaleY {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float ScaleZ {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
