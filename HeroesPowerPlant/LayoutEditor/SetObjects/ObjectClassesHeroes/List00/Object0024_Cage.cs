using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0024_Cage : SetObjectHeroes
    {
        public enum ECageType : byte
        {
            PFixed = 0,
            PFlying = 1,
            UFixed = 2,
            UFlying = 3
        }

        public ECageType CageType { get; set; }
        public float MoveCycleSec { get; set; }
        public float MoveRangeH { get; set; }
        public float MoveRangeV { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            CageType = (ECageType)reader.ReadInt32();
            MoveCycleSec = reader.ReadSingle();
            MoveRangeH = reader.ReadSingle();
            MoveRangeV = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((int)CageType);
            writer.Write(MoveCycleSec);
            writer.Write(MoveRangeH);
            writer.Write(MoveRangeV);
        }
    }
}