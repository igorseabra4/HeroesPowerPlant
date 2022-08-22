using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1540_EggHammer : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Normal = 0,
            Helmet = 1
        }

        public EEnemyType EnemyType { get; set; }
        public EAppear Appear { get; set; }
        public float MoveSpeed { get; set; }
        public float MoveRange { get; set; }
        public float ScopeRange { get; set; }
        public float ScopeOffset { get; set; }
        public int AttackInterval { get; set; }
        public float WeaponSpeed { get; set; }
        public float FallDistance { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            EnemyType = (EEnemyType)reader.ReadByte();
            Appear = (EAppear)reader.ReadByte();
            reader.BaseStream.Position += 2;
            MoveSpeed = reader.ReadSingle();
            MoveRange = reader.ReadSingle();
            ScopeRange = reader.ReadSingle();
            ScopeOffset = reader.ReadSingle();
            AttackInterval = reader.ReadInt32();
            WeaponSpeed = reader.ReadSingle();
            FallDistance = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)EnemyType);
            writer.Write((byte)Appear);
            writer.Pad(2);
            writer.Write(MoveSpeed);
            writer.Write(MoveRange);
            writer.Write(ScopeRange);
            writer.Write(ScopeOffset);
            writer.Write(AttackInterval);
            writer.Write(WeaponSpeed);
            writer.Write(FallDistance);
        }
    }
}
