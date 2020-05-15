using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object1137_StretchGrass : SetObjectShadow {
        //SetStretchGrass(level, angle, length, charge, NOBI_KUSA)
        //HPP name: Slingshot Plant
        public float Strength {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Angle {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float NoControlTimeAfterLaunch {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("How far back the sling stretches")]
        public float Length {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Time to wait after fully pulled back Length amount before launch")]
        public float Charge {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}
