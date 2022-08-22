using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1590_RhinoLiner : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Standard = 0,
            Attack = 1
        }

        public enum EPathType : byte
        {
            Standard = 0,
            Loop = 1
        }

        public enum EIronBallType : byte
        {
            Homing = 0,
            NotHoming = 1
        }

        public EEnemyType EnemyType { get; set; }
        public EPathType PathType { get; set; }
        public EIronBallType IronBallType { get; set; }
        public float WeaponSpeed { get; set; }
        public int AttackInterval { get; set; }
        public float MoveSpeedMin { get; set; }
        public float MoveSpeed { get; set; }
        public float MoveSpeedMax { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            EnemyType = (EEnemyType)reader.ReadByte();
            PathType = (EPathType)reader.ReadByte();
            IronBallType = (EIronBallType)reader.ReadByte();
            reader.BaseStream.Position += 1;
            WeaponSpeed = reader.ReadSingle();
            AttackInterval = reader.ReadInt32();
            MoveSpeedMin = reader.ReadSingle();
            MoveSpeed = reader.ReadSingle();
            MoveSpeedMax = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)EnemyType);
            writer.Write((byte)PathType);
            writer.Write((byte)IronBallType);
            writer.Write((byte)0);
            writer.Write(WeaponSpeed);
            writer.Write(AttackInterval);
            writer.Write(MoveSpeedMin);
            writer.Write(MoveSpeed);
            writer.Write(MoveSpeedMax);
        }
    }
}
