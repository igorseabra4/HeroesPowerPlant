using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002C_RollDoor : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float Elevation
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public Int16 NoControlTime
        {
            get { return ReadWriteWord(12); }
            set { ReadWriteWord(12, value); }
        }
    }
}