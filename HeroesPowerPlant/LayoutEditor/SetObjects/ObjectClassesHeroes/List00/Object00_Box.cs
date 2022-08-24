using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Box : SetObjectHeroes
    {
        public enum ECrashMode : short
        {
            CrashOut = 0,
            CrashThrough = 1
        }

        [MiscSetting]
        public ECrashMode CrashMode { get; set; }
        [MiscSetting, Description("Usually 0")]
        public short Unknown1 { get; set; }
        [MiscSetting, Description("Usually 0")]
        public short Unknown2 { get; set; }
    }
}