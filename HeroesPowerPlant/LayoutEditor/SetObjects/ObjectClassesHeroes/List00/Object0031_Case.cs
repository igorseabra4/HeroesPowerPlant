using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0031_Case : SetObjectHeroes
    {
        public enum EDirection : byte
        {
            Up = 0,
            Down = 1,
        }

        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
        [MiscSetting, Description("Doesn't use actual Link ID. Use this one.")]
        public byte LinkID { get; set; }
        [MiscSetting]
        public EDirection Direction { get; set; }
    }
}