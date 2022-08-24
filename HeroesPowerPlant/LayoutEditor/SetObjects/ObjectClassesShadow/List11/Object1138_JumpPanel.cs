namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1138_JumpPanel : SetObjectShadow
    {
        //Catapult(model, level, nocontrol time (sec))
        [MiscSetting]
        public int Model { get; set; }
        [MiscSetting]
        public float Strength { get; set; }
        [MiscSetting]
        public float NoControlTimeAfterLaunch { get; set; }
        [MiscSetting]
        public float AngleX { get; set; }
        [MiscSetting]
        public float Unused_AngleY { get; set; }
        [MiscSetting]
        public float AngleZ { get; set; }
    }
}
