using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020A_ColliQuake : SetObjectManagerHeroes
    {
        public float ScaleX
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Strenght
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public Int32 Time
        {
            get { return ReadWriteLong(20); }
            set { ReadWriteLong(20, value); }
        }
    }
}
