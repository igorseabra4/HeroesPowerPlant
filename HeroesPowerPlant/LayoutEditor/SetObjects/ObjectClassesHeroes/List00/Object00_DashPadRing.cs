using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_DashPadRing : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public Int16 ControlTime
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }
    }
}