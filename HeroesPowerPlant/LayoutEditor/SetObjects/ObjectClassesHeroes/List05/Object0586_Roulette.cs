using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0586_Roulette : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public Int32 Speed
        {
            get { return ReadWriteLong(8); }
            set { ReadWriteLong(8, value); }
        }
    }
}