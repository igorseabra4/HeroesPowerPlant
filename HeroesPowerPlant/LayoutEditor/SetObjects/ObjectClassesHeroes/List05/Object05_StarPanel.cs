using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_StarPanel : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public Int32 Color
        {
            get { return ReadLong(8); }
            set { Write(8, value); }
        }
    }
}