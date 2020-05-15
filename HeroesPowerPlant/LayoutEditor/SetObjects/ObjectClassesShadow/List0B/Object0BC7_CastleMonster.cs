using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0BC7_CastleMonster : SetObjectShadow {
        //Monster(MIN_SPPED m/sec, MAX_SPPED m/sec, ACCEL m/sec^2, DECEL m/sec^2)
        
        [Description("m/sec")]
        public float MinSpeed {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        [Description("m/sec")]
        public float MaxSpeed {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("m/sec^2")]
        public float Accel {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("m/sec^2")]
        public float Decel {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
