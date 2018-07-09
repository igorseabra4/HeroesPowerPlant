namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0005_Switch : SetObjectManagerHeroes
    {
        public enum SwitchType : byte
        {
            Alternate = 0,
            Touch = 1,
            Once = 2,
            Interlock = 3
        }

        public SwitchType Type
        {
            get { return (SwitchType)ReadWriteByte(4); }
            set { ReadWriteByte(4, (byte)value); }
        }

        public bool Hidden
        {
            get { return ReadWriteByte(5) != 0; }
            set { ReadWriteByte(5, value ? (byte)1 : (byte)0); }
        }

        public byte LinkIDforHidden
        {
            get { return ReadWriteByte(6); }
            set { ReadWriteByte(6, value); }
        }

        public enum SoundType : byte
        {
            Pi = 0,
            Pipori = 1
        }
        public SoundType Sound
        {
            get { return (SoundType)ReadWriteByte(7); }
            set { byte a = (byte)value; ReadWriteByte(7, a); }
        }
    }
}