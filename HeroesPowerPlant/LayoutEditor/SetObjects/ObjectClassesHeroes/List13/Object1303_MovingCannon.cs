using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1303_MovingCannon : SetObjectManagerHeroes
    {
        public float MaxHeight
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }
    }
}