using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000E_Checkpoint : SetObjectHeroes
    {
        public short Priority { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Priority = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Priority);
        }
    }
}