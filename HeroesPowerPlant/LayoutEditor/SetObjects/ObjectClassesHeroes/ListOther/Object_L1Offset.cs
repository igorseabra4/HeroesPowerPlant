using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Offset : SetObjectHeroes
    {
        public int Offset { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Offset = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Offset);
        }
    }
}