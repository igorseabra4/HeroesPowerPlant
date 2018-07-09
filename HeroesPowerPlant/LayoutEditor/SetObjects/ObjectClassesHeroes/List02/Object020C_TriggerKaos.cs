using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020C_TriggerKaos : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public byte Type
        {
            get { return ReadWriteByte(8); }
            set { ReadWriteByte(8, value); }
        }

        public byte Param2
        {
            get { return ReadWriteByte(9); }
            set { ReadWriteByte(9, value); }
        }

        public float Param3
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}
