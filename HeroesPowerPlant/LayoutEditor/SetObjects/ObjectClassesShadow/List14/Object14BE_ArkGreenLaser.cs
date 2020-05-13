using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object14BE_ArkGreenLaser : SetObjectShadow {
        //ArkLaser

        [Description("Y-axis increase up/down half of this value, split equally")]
        public float Length {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        [Description("How long the laser runs (seconds) once active")]
        public float ActiveTime {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("How long the laser waits (seconds) before firing again")]
        public float RechargeTime {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
