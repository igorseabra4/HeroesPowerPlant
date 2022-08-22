using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0029_Pole : SetObjectHeroes
    {
        public short Length { get; set; }
        public short Range { get; set; }
        public short StartOffset { get; set; }
        public short EndOffset { get; set; }
        public float ReleaseElevation { get; set; }
        public float ReleaseAzimuth { get; set; }
        public float ReleasePower { get; set; }
        public byte UseReference { get; set; }
        public byte ReferenceID { get; set; }
        public short NoControlTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Length = reader.ReadInt16();
            Range = reader.ReadInt16();
            StartOffset = reader.ReadInt16();
            EndOffset = reader.ReadInt16();
            ReleaseElevation = reader.ReadSingle();
            ReleaseAzimuth = reader.ReadSingle();
            ReleasePower = reader.ReadSingle();
            UseReference = reader.ReadByte();
            ReferenceID = reader.ReadByte();
            NoControlTime = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Length);
            writer.Write(Range);
            writer.Write(StartOffset);
            writer.Write(EndOffset);
            writer.Write(ReleaseElevation);
            writer.Write(ReleaseAzimuth);
            writer.Write(ReleasePower);
            writer.Write(UseReference);
            writer.Write(ReferenceID);
            writer.Write(NoControlTime);
        }
    }
}