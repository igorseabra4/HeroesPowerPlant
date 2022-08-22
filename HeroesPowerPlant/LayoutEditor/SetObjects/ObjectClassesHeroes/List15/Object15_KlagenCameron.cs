using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15_KlagenCameron : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Normal = 0,
            Golden = 1
        }

        public enum EAppear : byte
        {
            Idle = 0,
            Walking = 1,
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        public EEnemyType EnemyType { get; set; }
        public EAppear Appear { get; set; }
        public float MoveRange { get; set; }
        public float ScopeRange { get; set; }
        public float ScopeOffset { get; set; }
        public short Unknown { get; set; }
        public short AttackInterval { get; set; }
        public float AttackSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            EnemyType = (EEnemyType)reader.ReadByte();
            Appear = (EAppear)reader.ReadByte();
            reader.BaseStream.Position += 2;
            MoveRange = reader.ReadSingle();
            ScopeRange = reader.ReadSingle();
            ScopeOffset = reader.ReadSingle();
            Unknown = reader.ReadInt16();
            AttackInterval = reader.ReadInt16();
            AttackSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)EnemyType);
            writer.Write((byte)Appear);
            writer.Pad(2);
            writer.Write(MoveRange);
            writer.Write(ScopeRange);
            writer.Write(ScopeOffset);
            writer.Write(Unknown);
            writer.Write(AttackInterval);
            writer.Write(AttackSpeed);
        }
    }
}
