using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020C_TriggerKaos : SetObjectManagerHeroes
    {
        public float Radius
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public UInt16 KaosNumber
        {
            get { return (ushort)ReadWriteWord(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}
