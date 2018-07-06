using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000E_Checkpoint : SetObjectManagerHeroes
    {
        public Int16 Priority
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteWord(4, value); }
        }
    }
}