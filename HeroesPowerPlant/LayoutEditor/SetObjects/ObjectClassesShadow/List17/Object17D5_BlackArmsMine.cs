using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object17D5_BlackArmsMine : SetObjectShadow {
        //BAMine(second)

        public float Translate_X {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Translate_Y {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Translate_Z {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Entire time it takes to translate from origin to Translate coords and back to origin.")]
        public float CycleTime {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
