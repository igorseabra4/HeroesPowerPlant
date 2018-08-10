namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1188_Curtain : SetObjectManagerHeroes
    {
        public enum CurtainType
        {
            Light = 0,
            Dark = 1,
        }

        public CurtainType Type
        {
            get { return (CurtainType)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public byte Pole
        {
            get { return ReadByte(5); }
            set { Write(5, value); }
        }

        public bool IsUpsideDown
        {
            get { return ReadByte(6) != 0; }
            set { Write(6, (byte)(value ? 1 : 0)); }
        }

        public float Scale
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}
