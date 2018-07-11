using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Type : SetObjectManagerHeroes
    {
        public Int32 Type
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }
    }
}