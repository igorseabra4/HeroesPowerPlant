using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020C_TriggerKaos : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public byte Type
        {
            get { return ReadByte(8); }
            set { Write(8, value); }
        }

        public byte Param2
        {
            get { return ReadByte(9); }
            set { Write(9, value); }
        }

        public float Param3
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}
