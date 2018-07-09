using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1302_HorizCannon : SetObjectManagerHeroes
    {
        public short ShootTime
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ShootRange
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public byte IgnoreCollision
        {
            get { return ReadWriteByte(12); }
            set { ReadWriteByte(12, value); }
        }
    }
}