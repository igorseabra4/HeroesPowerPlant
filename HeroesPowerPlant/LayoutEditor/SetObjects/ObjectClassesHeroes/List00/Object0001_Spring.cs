using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_Spring : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public Int16 NoControlTime
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public float GuideLine
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}
