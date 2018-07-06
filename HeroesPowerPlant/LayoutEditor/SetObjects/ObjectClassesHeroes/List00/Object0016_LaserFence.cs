using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0016_LaserFence : SetObjectManagerHeroes
    {
        public enum TypeType
        {
            Fixed = 0,
            Intermittent = 1,
            Switch = 2,
            Scan = 3,
            Enemy = 4
        }

        public TypeType Type
        {
            get { return (TypeType)ReadWriteLong(4); }
            set { ReadWriteLong(4, (int)value); }
        }

        public float Lenght
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Width
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public Int32 Null_Interval_SwitchID_Speed_EnemyID
        {
            get { return ReadWriteLong(16); }
            set { ReadWriteLong(16, value); }
        }
    }
}