using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0033_EnergyCore : SetObjectShadow
    {
        public EEnergyCoreType CoreType { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            CoreType = (EEnergyCoreType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)CoreType);
        }
    }
}

