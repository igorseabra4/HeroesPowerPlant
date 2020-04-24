namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0302_RoadCap : SetObjectHeroes
    {
        public byte RoadCapType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public short ScaleX
        {
            get => ReadShort(6);
            set => Write(6, value);
        }

        public short ScaleY
        {
            get => ReadShort(8);
            set => Write(8, value);
        }
    }
}