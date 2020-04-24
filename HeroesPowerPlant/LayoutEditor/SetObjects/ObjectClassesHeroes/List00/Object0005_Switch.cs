namespace HeroesPowerPlant.LayoutEditor
{
    public enum SwitchType : byte
    {
        Alternate = 0,
        Touch = 1,
        Once = 2,
        Interlock = 3
    }

    public class Object0005_Switch : SetObjectHeroes
    {
        public SwitchType SwitchType
        {
            get => (SwitchType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public bool Hidden
        {
            get => ReadByte(5) != 0;
            set => Write(5, value ? (byte)1 : (byte)0);
        }

        public byte LinkIDforHidden
        {
            get => ReadByte(6);
            set => Write(6, value);
        }

        public enum SoundType : byte
        {
            Pi = 0,
            Pipori = 1
        }
        public SoundType Sound
        {
            get => (SoundType)ReadByte(7);
            set => Write(7, (byte) value);
        }
    }
}