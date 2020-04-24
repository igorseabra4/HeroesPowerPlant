namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0025_FormSign : SetObjectHeroes
    {
        public Formation Formation
        {
            get => (Formation)ReadByte(4);
            set => Write(4, (byte)value);
        }
    }
}