namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D7_DigitalBreakableTile : SetObjectShadow
    {
        //ElecCristalWall(type<Horizontal,Vertical>, AppearAngleX, AppearAngleY)

        [MiscSetting]
        public EDirection DirectionType { get; set; }
        [MiscSetting]
        public float AppearAngleX { get; set; }
        [MiscSetting]
        public float AppearAngleY { get; set; }
    }
}
