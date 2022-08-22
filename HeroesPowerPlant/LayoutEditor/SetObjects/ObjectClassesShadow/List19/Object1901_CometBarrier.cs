using System.ComponentModel;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1901_CometBarrier : SetObjectShadow
    {
        //BAShield

        [Description("Requires a Comet Barrier Switch object with same Link ID\nOtherwise no barrier is displayed")]
        public BAShield_BarrierType BarrierType { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            BarrierType = (BAShield_BarrierType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)BarrierType);
        }
    }

    public enum BAShield_BarrierType
    {
        Hero,
        Dark,
        Shadow
    }
}
