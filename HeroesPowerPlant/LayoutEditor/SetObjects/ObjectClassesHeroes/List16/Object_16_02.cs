using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_02 : SetObjectManagerHeroes
    {
        public float CoreHP
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ShieldHP
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float MissleHP
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}
