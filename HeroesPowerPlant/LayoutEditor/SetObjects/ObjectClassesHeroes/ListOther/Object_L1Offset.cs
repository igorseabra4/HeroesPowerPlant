using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Offset : SetObjectManagerHeroes
    {
        public Int32 Offset
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }
    }
}