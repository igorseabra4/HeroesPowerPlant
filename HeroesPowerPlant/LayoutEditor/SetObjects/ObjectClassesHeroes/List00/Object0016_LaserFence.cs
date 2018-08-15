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
            get { return (TypeType)ReadLong(4); }
            set { Write(4, (int)value); }
        }

        public float Lenght
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Width
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public int Null_Interval_SwitchID_Speed_EnemyID
        {
            get { return ReadLong(16); }
            set { Write(16, value); }
        }
    }
}