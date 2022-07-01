using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1901_CometBarrier : SetObjectShadow
    {
        //BAShield

        [Description("Requires a Comet Barrier Switch object with same Link ID\nOtherwise no barrier is displayed")]
        public BAShield_BarrierType BarrierType
        {
            get => (BAShield_BarrierType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum BAShield_BarrierType
    {
        Hero,
        Dark,
        Shadow
    }
}
