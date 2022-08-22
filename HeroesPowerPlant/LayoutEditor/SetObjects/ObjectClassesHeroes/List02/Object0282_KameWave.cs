using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0282_KameWave : SetObjectHeroes
    {
        public byte KameWaveType { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            KameWaveType = reader.ReadByte();
            reader.BaseStream.Position += 3;
            Speed = reader.ReadSingle();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(KameWaveType);
            writer.Pad(3);
            writer.Write(Speed);
            writer.Write(Scale);
        }
    }
}
