namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectHeroes
    {
        public short LineToPlay
        {
            get => ReadShort(4);
            set => Write(4, value);
        }

        public bool DeleteByLinkOff
        {
            get => ReadByte(6) != 0;
            set => Write(6, value ? (byte)1 : (byte)0);
        }
    }
}