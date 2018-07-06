using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_StarPanel : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public Int32 Color
        {
            get { return ReadWriteLong(8); }
            set { ReadWriteLong(8, value); }
        }
    }
}