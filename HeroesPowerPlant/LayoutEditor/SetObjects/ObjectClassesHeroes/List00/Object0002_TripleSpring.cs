using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0002_TripleSpring : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public Int16 NoControlTime
        {
            get { return ReadWriteWord(12); }
            set { ReadWriteWord(12, value); }
        }

        public ItemType Item
        {
            get { return (ItemType)ReadWriteByte(14); }
            set { ReadWriteByte(14, (byte)value); }
        }
    }
}
