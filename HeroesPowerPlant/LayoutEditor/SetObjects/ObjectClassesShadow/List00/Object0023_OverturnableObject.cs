using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0023_OverturnableObject : SetObjectShadow
    {
        public int ModelIfMultiple { get; set; }
        public int UnusedInt { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            ModelIfMultiple = reader.ReadInt32();
            UnusedInt = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(ModelIfMultiple);
            writer.Write(UnusedInt);
        }
    }
}
