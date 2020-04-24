using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_00_01 : SetObjectHeroes
    {
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int Refresh
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public int Disable
        {
            get => ReadInt(12);
            set => Write(12, value);
        }
    }
}
