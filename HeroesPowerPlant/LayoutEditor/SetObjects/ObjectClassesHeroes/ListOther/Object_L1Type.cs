using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Type : SetObjectManagerHeroes
    {
        public Int32 Type
        {
            get { return ReadWriteLong(4); }
            set { ReadWriteLong(4, value); }
        }
    }
}