using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050B_Slot : SetObjectManagerHeroes
    {
        public Int32 Rate
        {
            get { return ReadWriteLong(4); }
            set { ReadWriteLong(4, value); }
        }
    }
}