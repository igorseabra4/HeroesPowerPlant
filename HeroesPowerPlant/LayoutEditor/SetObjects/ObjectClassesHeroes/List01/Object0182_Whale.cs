using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0182_Whale : SetObjectManagerHeroes
    {
        public byte WhaleType
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public Int16 TriggerSize
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public float WhaleScale
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ArchRadius
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float TriggerX
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float TriggerY
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public float TriggerZ
        {
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }
    }
}
