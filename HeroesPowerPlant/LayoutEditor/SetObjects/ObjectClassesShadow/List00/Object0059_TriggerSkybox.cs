using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0059_TriggerSkybox : SetObjectShadow {

        // 0 unk (always 0)
        // 1 int
        // 2 float
        // 3 int
        // 4 int
        // 5 int
        // 6 int

        [Description("Always 0 in original objects, purpose unknown")]
        public int Unknown0 {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public int int1 {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float float2 {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public int int3 {
            get => ReadInt(12);
            set => Write(12, value);
        }
        public int int4 {
            get => ReadInt(16);
            set => Write(16, value);
        }
        public int int5 {
            get => ReadInt(20);
            set => Write(20, value);
        }
        public int int6 {
            get => ReadInt(24);
            set => Write(24, value);
        }

    }
}

