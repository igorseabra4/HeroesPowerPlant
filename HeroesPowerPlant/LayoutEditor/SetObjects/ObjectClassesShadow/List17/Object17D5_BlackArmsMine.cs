using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object17D5_BlackArmsMine : SetObjectShadow
    {
        //BAMine(second)

        [MiscSetting]
        public float Translate_X { get; set; }
        [MiscSetting]
        public float Translate_Y { get; set; }
        [MiscSetting]
        public float Translate_Z { get; set; }
        [MiscSetting, Description("Entire time it takes to translate from origin to Translate coords and back to origin.")]
        public float CycleTime { get; set; }
    }
}
