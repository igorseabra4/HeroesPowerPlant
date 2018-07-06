using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0302_RoadCap : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public Int16 ScaleX
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public Int16 ScaleY
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }
    }
}