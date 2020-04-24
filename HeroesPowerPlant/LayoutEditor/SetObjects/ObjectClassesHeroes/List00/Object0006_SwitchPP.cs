namespace HeroesPowerPlant.LayoutEditor
{
    public enum SwitchMode : byte
    {
        Push = 0,
        Pull = 1
    }

    public class Object0006_SwitchPP : SetObjectHeroes
    {
        public SwitchMode SwitchMode
        {
            get => (SwitchMode)ReadByte(4);
            set => Write(4, (byte)value);
        }
    }
}