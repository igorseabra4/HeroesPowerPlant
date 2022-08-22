using HeroesPowerPlant.Shared.Utilities;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0016_LaserFence : SetObjectHeroes
    {
        public enum ELaserFenceType : int
        {
            Fixed = 0,
            Intermittent = 1,
            Switch = 2,
            Scan = 3,
            Enemy = 4
        }

        public ELaserFenceType LaserFenceType { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        private int Setting4;

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            LaserFenceType = (ELaserFenceType)reader.ReadInt32();
            Length = reader.ReadSingle();
            Width = reader.ReadSingle();
            Setting4 = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((int)LaserFenceType);
            writer.Write(Length);
            writer.Write(Width);
            writer.Write(Setting4);
        }

        private const string desc = "Interval, SwitchID, Speed and EnemyID are actually the same setting. Which one is used depends on LaserFenceType.";

        [Description(desc)] public int Interval { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int SwitchID { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int Speed    { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int EnemyID  { get => Setting4; set => Setting4 = value; }
    }
}