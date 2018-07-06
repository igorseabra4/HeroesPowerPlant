using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_00_01 : SetObjectManagerHeroes
    {
        public float Radius
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public Int32 Refresh
        {
            get { return ReadWriteLong(8); }
            set { ReadWriteLong(8, value); }
        }

        public Int32 Disable
        {
            get { return ReadWriteLong(12); }
            set { ReadWriteLong(12, value); }
        }
    }
}
