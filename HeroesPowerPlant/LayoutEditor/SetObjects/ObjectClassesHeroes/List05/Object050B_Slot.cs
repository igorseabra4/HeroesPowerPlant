using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050B_Slot : SetObjectHeroes
    {
        public int Rate { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Rate = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Rate);
        }
    }
}