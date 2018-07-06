using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_Spring : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public Int16 NoControlTime
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public float GuideLine
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}
