namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_BallSwitch : SetObjectShadow
    {
        public enum EActivateType : int
        {
            OnOff = 0,
            OnTouch = 1,
            OnAlways = 2,
            Decoration = 3
        }

        [MiscSetting]
        public EActivateType ActivateType { get; set; }
    }

}

