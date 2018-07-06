using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_IntTypeFloatScale : SetObjectManagerHeroes
    {
        public Int32 Type
        {
            get { return ReadWriteLong(4); }
            set { ReadWriteLong(4, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}