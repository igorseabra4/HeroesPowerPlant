using HeroesPowerPlant.Shared.Utilities;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0913_RainCollision : SetObjectHeroes
    {
        public float Scale { get; set; }
        [Description("16 entries")]
        public byte[] RainIDs { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
            RainIDs = reader.ReadBytes(16);
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
            var count = 0;
            foreach (var i in RainIDs)
            {
                writer.Write(i);
                count++;
                if (count == 16)
                    break;
            }
        }
    }
}