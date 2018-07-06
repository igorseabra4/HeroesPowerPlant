using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectManagerHeroes
    {
        public float SpeedHorizontal
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float SpeedVertical
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public Int16 ControlTime
        {
            get { return ReadWriteWord(12); }
            set { ReadWriteWord(12, value); }
        }
    }
}