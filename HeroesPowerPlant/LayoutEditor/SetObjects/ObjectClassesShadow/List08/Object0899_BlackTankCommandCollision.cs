using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0899_BlackTankCommandCollision : SetObjectShadow
    {
        // Same as EscapePodCommandCollision
        public float DetectX
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float DetectY
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float DetectZ
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Move - Use to cancel a Stop command with param0 set to 0\nStop - param0 amount of seconds to be stopped (int), 0 is indefinite (Move needed)\nAccelerate - param0 amount of seconds to reach speed in param1; param1 speed/travel rate, larger is faster\nSetPos - param0 spline index; param1 position in percentage of spline (0.0 = start, 1.0 = end)")]
        public BlackTankActionID ActionID
        {
            get => (BlackTankActionID)ReadInt(12);
            set => Write(12, (int)value);
        }

        [Description("Known types (values unknown): no timer, route, Not in use, speed, rate(0.0-1.0), type")]
        public float ActionParam0
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Known types (values unknown): no timer, route, Not in use, speed, rate(0.0-1.0), type")]
        public float ActionParam1
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public enum BlackTankActionID
        {
            Move,
            Stop,
            Accelerate,
            SetPos
        }
    }
}
