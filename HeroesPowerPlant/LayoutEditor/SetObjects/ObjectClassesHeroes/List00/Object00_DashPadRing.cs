using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_DashPadRing : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public Int16 ControlTime
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }
    }
}