using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectManagerHeroes
    {
        public Int16 LineToPlay
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteWord(4, value); }
        }

        public bool DeleteByLinkOff
        {
            get { return ReadWriteByte(6) != 0; }
            set { ReadWriteByte(6, value ? (byte)1 : (byte)0); }
        }
    }
}