using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0066_TriggerBobJump : SetObjectHeroes
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public float RunDistance { get; set; }
        public float JumpDistance { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Width = reader.ReadSingle();
            Height = reader.ReadSingle();
            RunDistance = reader.ReadSingle();
            JumpDistance = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(RunDistance);
            writer.Write(JumpDistance);
        }
    }
}