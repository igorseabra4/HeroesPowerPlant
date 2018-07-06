using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1305_EggFleetDoor : SetObjectManagerHeroes
    {
        public float TriggerX
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float TriggerY
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float TriggerZ
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public Int16 TriggerXSize
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteWord(4, value); }
        }

        public Int16 TriggerYSize
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public Int16 TriggerZSize
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public Int16 TriggerYRot
        {
            get { return ReadWriteWord(24); }
            set { ReadWriteWord(24, value); }
        }
    }
}