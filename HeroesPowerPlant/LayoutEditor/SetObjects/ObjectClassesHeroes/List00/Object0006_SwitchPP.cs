namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0006_SwitchPP : SetObjectManagerHeroes
    {
        public enum SwitchMode : byte
        {
            Push = 0,
            Pull = 1
        }

        public SwitchMode Mode
        {
            get { return (SwitchMode)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }
    }
}