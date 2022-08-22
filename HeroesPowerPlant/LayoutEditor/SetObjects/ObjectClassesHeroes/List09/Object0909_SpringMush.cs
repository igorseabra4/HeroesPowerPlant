using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0909_SpringMush : SetObjectHeroes
    {
        public float Speed { get; set; }
        public byte NoControlTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Speed = reader.ReadSingle();
            NoControlTime = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Speed);
            writer.Write(NoControlTime);
        }
    }
}