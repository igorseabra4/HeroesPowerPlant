using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0302_RoadCap : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public Int16 ScaleX
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public Int16 ScaleY
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }
    }
}