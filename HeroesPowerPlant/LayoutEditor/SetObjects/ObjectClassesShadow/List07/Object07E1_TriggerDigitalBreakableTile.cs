namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07E1_TriggerDigitalBreakableTile : SetObjectShadow
    {
        //ElecCristalWallSwitch(range)
        public float Range
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
    }
}
