namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1302_HorizCannon : SetObjectManagerHeroes
    {
        public short ShootTime
        {
            get { return ReadShort(4); }
            set { Write(4, (float)value); }
        }

        public float ShootRange
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public byte IgnoreCollision
        {
            get { return ReadByte(12); }
            set { Write(12, value); }
        }
    }
}