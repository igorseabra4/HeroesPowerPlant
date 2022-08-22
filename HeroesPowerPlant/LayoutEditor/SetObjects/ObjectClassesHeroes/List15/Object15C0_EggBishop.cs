using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15C0_EggBishop : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Bishop = 0,
            Magician = 1
        }

        public EEnemyType EnemyType { get; set; }
        public float MoveRange { get; set; }
        public float ScopeRange { get; set; }
        public float ScopeOffset { get; set; }
        public int AttackInterval { get; set; }
        public float MoveSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            EnemyType = (EEnemyType)reader.ReadByte();
            reader.BaseStream.Position += 3;
            MoveRange = reader.ReadSingle();
            ScopeRange = reader.ReadSingle();
            ScopeOffset = reader.ReadSingle();
            AttackInterval = reader.ReadInt32();
            MoveSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)EnemyType);
            writer.Pad(3);
            writer.Write(MoveRange);
            writer.Write(ScopeRange);
            writer.Write(ScopeOffset);
            writer.Write(AttackInterval);
            writer.Write(MoveSpeed);
        }
    }
}
