using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_00_01 : SetObjectManagerHeroes
    {
        public float Radius
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public Int32 Refresh
        {
            get { return ReadLong(8); }
            set { Write(8, value); }
        }

        public Int32 Disable
        {
            get { return ReadLong(12); }
            set { Write(12, value); }
        }
    }
}
