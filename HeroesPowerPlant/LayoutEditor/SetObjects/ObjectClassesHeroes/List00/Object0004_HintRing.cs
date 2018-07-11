using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectManagerHeroes
    {
        public Int16 LineToPlay
        {
            get { return ReadShort(4); }
            set { Write(4, value); }
        }

        public bool DeleteByLinkOff
        {
            get { return ReadByte(6) != 0; }
            set { Write(6, value ? (byte)1 : (byte)0); }
        }
    }
}