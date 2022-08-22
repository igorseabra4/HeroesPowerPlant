using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_02 : SetObjectHeroes
    {
        public float CoreHP { get; set; }
        public float ShieldHP { get; set; }
        public float MissleHP { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            CoreHP = reader.ReadSingle();
            ShieldHP = reader.ReadSingle();
            MissleHP = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(CoreHP);
            writer.Write(ShieldHP);
            writer.Write(MissleHP);
        }
    }
}
