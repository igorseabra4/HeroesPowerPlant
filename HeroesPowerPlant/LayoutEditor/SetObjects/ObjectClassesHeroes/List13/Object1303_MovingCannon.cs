using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1303_MovingCannon : SetObjectManagerHeroes
    {
        public float MaxHeight
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }
    }
}