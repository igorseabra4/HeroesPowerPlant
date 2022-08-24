using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1901_CometBarrier : SetObjectShadow
    {
        public enum EBarrierType : int
        {
            Hero,
            Dark,
            Shadow
        }

        [MiscSetting, Description("Requires a Comet Barrier Switch object with same Link ID\nOtherwise no barrier is displayed")]
        public EBarrierType BarrierType { get; set; }
    }
}
