namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1302_HorizCannon : SetObjectHeroes
    {
        public short ShootTime
        {
            get => ReadShort(4);
            set => Write(4, (float)value);
        }

        public float ShootRange
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public byte IgnoreCollision
        {
            get => ReadByte(12);
            set => Write(12, value);
        }
    }
}