using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D8_LaserWallBarrier : SetObjectShadow
    {
        public enum EBarrierType
        {
            Model0,
            Model1,
            Model2
        }
        //ElecBarrier

        [MiscSetting, Description("Note: One sided view only, currently opposite of placement\n Move around to see model in editor")]
        public EBarrierType BarrierType { get; set; }
    }
}
