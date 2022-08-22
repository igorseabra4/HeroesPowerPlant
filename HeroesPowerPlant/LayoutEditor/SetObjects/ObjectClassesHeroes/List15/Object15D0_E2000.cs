using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15D0_E2000 : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Normal = 0,
            Special = 1
        }

        public EEnemyType EnemyType { get; set; }
        public EAppear Appear { get; set; }
        public float MoveRange { get; set; }
        public float ScopeRange { get; set; }
        public float ScopeOffset { get; set; }
        public int AttackInterval { get; set; }
        public int AttackFrame { get; set; }
        public float Distance { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            EnemyType = (EEnemyType)reader.ReadByte();
            Appear = (EAppear)reader.ReadByte();
            reader.BaseStream.Position += 2;
            MoveRange = reader.ReadSingle();
            ScopeRange = reader.ReadSingle();
            ScopeOffset = reader.ReadSingle();
            AttackInterval = reader.ReadInt32();
            AttackFrame = reader.ReadInt32();
            Distance = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)EnemyType);
            writer.Write((byte)Appear);
            writer.Pad(2);
            writer.Write(MoveRange);
            writer.Write(ScopeRange);
            writer.Write(ScopeOffset);
            writer.Write(AttackInterval);
            writer.Write(AttackFrame);
            writer.Write(Distance);
        }
    }
}
