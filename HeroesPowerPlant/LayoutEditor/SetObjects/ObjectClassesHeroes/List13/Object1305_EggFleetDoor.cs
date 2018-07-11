using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1305_EggFleetDoor : SetObjectManagerHeroes
    {
        public float TriggerX
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float TriggerY
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float TriggerZ
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public Int16 TriggerXSize
        {
            get { return ReadShort(4); }
            set { Write(4, value); }
        }

        public Int16 TriggerYSize
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public Int16 TriggerZSize
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public Int16 TriggerRotY
        {
            get { return ReadShort(24); }
            set { Write(24, value); }
        }
    }
}