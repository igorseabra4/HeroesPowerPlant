using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0005_Checkpoint : SetObjectShadow
    {
        public int Number { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Number = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }
}
