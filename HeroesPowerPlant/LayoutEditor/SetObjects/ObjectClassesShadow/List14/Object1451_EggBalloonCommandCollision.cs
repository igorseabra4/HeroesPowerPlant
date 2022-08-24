using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1451_EggBalloonCommandCollision : SetObjectShadow
    {
        public enum EActionID : int
        {
            Move,
            Stop,
            MissileAttack,
            BombAttack,
            Unknown4,
            Unknown5,
            Accelerate,
            SetPos
        }

        //CommandCollision

        [MiscSetting]
        public float DetectX { get; set; }
        [MiscSetting]
        public float DetectY { get; set; }
        [MiscSetting]
        public float DetectZ { get; set; }
        [MiscSetting, Description("Move - Use to cancel a Stop command with param0 set to 0\nStop - param0 amount of seconds to be stopped (int), 0 is indefinite (Move needed)\nMissileAttack and BombAttack - param0 amount of seconds to attack; param1 - amount of seconds to wait each attack\nAccelerate - param0 amount of seconds to reach speed in param1; param1 speed/travel rate, larger is faster\nSetPos - param0 spline index; param1 position in percentage of spline (0.0 = start, 1.0 = end)")]
        public EActionID ActionID { get; set; }
        [MiscSetting, Description("Known types (values unknown): no timer, route, Not in use, speed, rate(0.0-1.0), type")]
        public float ActionParam0 { get; set; }
        [MiscSetting, Description("Known types (values unknown): no timer, route, Not in use, speed, rate(0.0-1.0), type")]
        public float ActionParam1 { get; set; }
    }
}
