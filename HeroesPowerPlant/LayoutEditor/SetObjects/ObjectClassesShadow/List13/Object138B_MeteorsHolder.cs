using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object138B_MeteorsHolder : SetObjectShadow
    {
        //MeteorsHolder(MOVE_SPD m/s, DIRECTION_X deg, DIRECTION_Y deg, ROT_SPD_X deg/s, ROT_SPD_Z deg/s, ROT_SPD_Y deg/s, LIFETIME s, METEOR_S)

        [MiscSetting, Description("m")]
        public float MaxTravelDistance { get; set; }

        [MiscSetting, Description("m/s")]
        public float MoveSpeed { get; set; }

        [MiscSetting, Description("deg")]
        public float Direction_X { get; set; }

        [MiscSetting, Description("deg")]
        public float Direction_Y { get; set; }

        [MiscSetting, Description("deg/s")]
        public float RotSpd_X { get; set; }

        [MiscSetting, Description("deg/s")]
        public float RotSpd_Z { get; set; }

        [MiscSetting, Description("deg/s")]
        public float RotSpd_Y { get; set; }

        [MiscSetting, Description("s\n Time until meteor explodes")]
        public float Lifetime { get; set; }
    }
}
