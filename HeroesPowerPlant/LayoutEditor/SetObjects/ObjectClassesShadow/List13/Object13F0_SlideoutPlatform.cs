namespace HeroesPowerPlant.LayoutEditor
{
    public class Object13F0_SlideoutPlatform : SetObjectShadow
    {
        //SetPopFooting(model sec_begin, sec_wait)
        [MiscSetting]
        public int Model { get; set; }
        [MiscSetting]
        public float SecBegin { get; set; }
        [MiscSetting]
        public float SecWait { get; set; }
    }
}
