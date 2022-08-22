using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0014_GoalRing : SetObjectShadow
    {
        public enum EEmeraldColor
        {
            Blue = 0,
            Green = 1,
            Purple = 2,
            Red = 3,
            Aqua = 4,
            Yellow = 5,
            White = 6,
            None = 7
        }

        public EEmeraldColor EmeraldColor { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            EmeraldColor = (EEmeraldColor)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)EmeraldColor);
        }
    }
}
