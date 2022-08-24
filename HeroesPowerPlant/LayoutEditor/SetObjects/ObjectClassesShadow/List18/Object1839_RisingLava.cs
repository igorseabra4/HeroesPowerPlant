namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1839_RisingLava : SetObjectShadow
    {
        public enum EModel : int
        {
            Model_1,
            Model_2,
            Model_3,
            Model_4,
            Model_5,
            Model_6,
            Model_7,
            Model_8,
            Model_9
        }

        //SetMagma
        [MiscSetting]
        public EModel Model { get; set; }
        [MiscSetting]
        public float RiseAmountMax { get; set; }
        [MiscSetting]
        public float RiseAmountPerSecond { get; set; }
    }
}
