using HeroesPowerPlant.Shared.Utilities;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_FloatingPlatform : SetObjectHeroes
    {
        public enum EPlatformType : byte
        {
            Fixed = 0,
            Moving = 1,
            Alternate = 2,
            Disappear = 3
        }

        [Description("Disappear mode is unused. Delay can be negative to make the platform faster.")]
        public EPlatformType PlatformType { get; set; }
        public bool AlternateModel { get; set; }
        public short UnknownAlternateRange0 { get; set; }
        public short UnknownAlternateRange1 { get; set; }
        public short XOffset { get; set; }
        public short YOffset { get; set; }
        public short ZOffset { get; set; }
        public short TimeCycleFrame { get; set; }
        public byte DisappearLinkID { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            PlatformType = (EPlatformType)reader.ReadByte();
            AlternateModel = reader.ReadByteBool();
            UnknownAlternateRange0 = reader.ReadInt16();
            UnknownAlternateRange1 = reader.ReadInt16();
            XOffset = reader.ReadInt16();
            YOffset = reader.ReadInt16();
            ZOffset = reader.ReadInt16();
            TimeCycleFrame = reader.ReadInt16();
            DisappearLinkID = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)PlatformType);
            writer.Write((byte)(AlternateModel ? 1 : 0));
            writer.Write(UnknownAlternateRange0);
            writer.Write(UnknownAlternateRange1);
            writer.Write(XOffset);
            writer.Write(YOffset);
            writer.Write(ZOffset);
            writer.Write(TimeCycleFrame);
            writer.Write(DisappearLinkID);
        }
    }
}
