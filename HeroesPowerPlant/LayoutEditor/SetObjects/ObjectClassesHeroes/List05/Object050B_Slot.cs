using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050B_Slot : SetObjectManagerHeroes
    {
        public Int32 Rate
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }
    }
}