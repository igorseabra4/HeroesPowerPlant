using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0900_Frog : SetObjectHeroes
    {
        public float JumpDirX { get; set; }
        public float JumpDirY { get; set; }
        public float JumpDirZ { get; set; }
        public float Radius { get; set; }
        public float Scale { get; set; }
        public float JumpCycle { get; set; }
        public short StopTimeSec { get; set; }
        public short LeaveTimeSec { get; set; }
        public bool IsBlack { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            JumpDirX = reader.ReadSingle();
            JumpDirY = reader.ReadSingle();
            JumpDirZ = reader.ReadSingle();
            Radius = reader.ReadSingle();
            Scale = reader.ReadSingle();
            JumpCycle = reader.ReadSingle();
            StopTimeSec = reader.ReadInt16();
            LeaveTimeSec = reader.ReadInt16();
            IsBlack = reader.ReadByteBool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(JumpDirX);
            writer.Write(JumpDirY);
            writer.Write(JumpDirZ);
            writer.Write(Radius);
            writer.Write(Scale);
            writer.Write(JumpCycle);
            writer.Write(StopTimeSec);
            writer.Write(LeaveTimeSec);
            writer.Write((byte)(IsBlack ? 1 : 0));
        }
    }
}