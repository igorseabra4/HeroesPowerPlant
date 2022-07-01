namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_BallSwitch : SetObjectShadow
    {
        // Switch

        public BallSwitchActivateType ActivateType
        {
            get => (BallSwitchActivateType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum BallSwitchActivateType
    {
        OnOff = 0,
        OnTouch = 1,
        OnAlways = 2,
        Decoration = 3
    }
}

