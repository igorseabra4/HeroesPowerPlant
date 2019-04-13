using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1305_EggFleetDoor : SetObjectManagerHeroes
    {
        public float TriggerX
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float TriggerY
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float TriggerZ
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public Int16 TriggerXSize
        {
            get => ReadShort(4);
            set => Write(4, value);
        }

        public Int16 TriggerYSize
        {
            get => ReadShort(6);
            set => Write(6, value);
        }

        public Int16 TriggerZSize
        {
            get => ReadShort(8);
            set => Write(8, value);
        }

        public Int16 TriggerRotY
        {
            get => ReadShort(24);
            set => Write(24, value);
        }
    }
}