namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0025_FormSign : SetObjectManagerHeroes
    {
        public Formation Formation
        {
            get => (Formation)ReadByte(4);
            set => Write(4, (byte)value);
        }
    }
}