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
            get { return (SwitchType)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public bool Hidden
        {
            get { return ReadByte(5) != 0; }
            set { Write(5, value ? (byte)1 : (byte)0); }
        }

        public byte LinkIDforHidden
        {
            get { return ReadByte(6); }
            set { Write(6, value); }
        }

        public enum SoundType : byte
        {
            Pi = 0,
            Pipori = 1
        }
        public SoundType Sound
        {
            get { return (SoundType)ReadByte(7); }
            set { byte a = (byte)value; Write(7, a); }
        }
    }
}