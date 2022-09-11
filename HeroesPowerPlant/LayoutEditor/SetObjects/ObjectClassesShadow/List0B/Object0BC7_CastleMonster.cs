using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BC7_CastleMonster : SetObjectShadow
    {
        //Monster(MIN_SPPED m/sec, MAX_SPPED m/sec, ACCEL m/sec^2, DECEL m/sec^2)

        [MiscSetting, Description("m/sec")]
        public float MinSpeed { get; set; }
        [MiscSetting, Description("m/sec")]
        public float MaxSpeed { get; set; }
        [MiscSetting, Description("m/sec^2")]
        public float Accel { get; set; }
        [MiscSetting, Description("m/sec^2")]
        public float Decel { get; set; }
    }
}
