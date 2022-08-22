using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0026_FormGate : SetObjectHeroes
    {
        public EFormation Formation { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Formation = (EFormation)reader.ReadByte();
            reader.BaseStream.Position += 3;
            Width = reader.ReadSingle();
            Height = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Formation);
            writer.Pad(3);
            writer.Write(Width);
            writer.Write(Height);
        }
    }
}