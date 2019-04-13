using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_00_01 : SetObjectManagerHeroes
    {
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int Refresh
        {
            get => ReadLong(8);
            set => Write(8, value);
        }

        public int Disable
        {
            get => ReadLong(12);
            set => Write(12, value);
        }
    }
}
