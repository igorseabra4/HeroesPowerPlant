using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectManagerHeroes
    {
        public float SpeedHorizontal
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float SpeedVertical
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public Int16 ControlTime
        {
            get { return ReadShort(12); }
            set { Write(12, value); }
        }
    }
}