using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0281_TurtleFeet : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}
