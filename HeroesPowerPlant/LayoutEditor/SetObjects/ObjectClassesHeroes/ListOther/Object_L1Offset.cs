using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Offset : SetObjectManagerHeroes
    {
        public Int32 Offset
        {
            get { return ReadWriteLong(4); }
            set { ReadWriteLong(4, value); }
        }
    }
}