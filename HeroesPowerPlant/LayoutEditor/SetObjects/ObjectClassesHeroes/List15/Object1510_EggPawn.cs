using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1510_EggPawn : SetObjectHeroes
    {
        public enum EStartMode : byte
        {
            Asleep = 0,
            Wandering = 1,
            Running = 2,
            Fall = 3,
            Warp = 4,
            Falco = 5,
            Searching = 6
        }

        public enum EEnemyType : byte
        {
            NormalFree = 0,
            NormalStand = 1,
            KingFree = 2,
            KingStand = 3,
            Casino1Free = 4,
            Casino1Stand = 5,
            Casino2Free = 6,
            Casino2Stand = 7
        }

        public enum EWeapon : byte
        {
            None = 0,
            Lance = 1,
            LaserCannon = 2,
            MGun90 = 3,
            MGun120 = 4,
            MGun150 = 5,
            MGun180 = 6
        }

        public enum EShield : byte
        {
            None = 0,
            Concrete = 1,
            Plain = 2,
            Spike = 3
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        public EStartMode StartMode { get; set; }
        public EEnemyType EnemyType { get; set; }
        public EWeapon Weapon { get; set; }
        public EShield Shield { get; set; }
        public short ScopeRange { get; set; }
        public short ScopeOffset { get; set; }
        public float MovingRange { get; set; }
        public float FallWarpHeight { get; set; }
        public float FalcoNumber { get; set; }
        public float ShotSpeed { get; set; }
        public int ShotInterval { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            StartMode = (EStartMode)reader.ReadByte();
            EnemyType = (EEnemyType)reader.ReadByte();
            Weapon = (EWeapon)reader.ReadByte();
            Shield = (EShield)reader.ReadByte();
            ScopeRange = reader.ReadInt16();
            ScopeOffset = reader.ReadInt16();
            MovingRange = reader.ReadSingle();
            FallWarpHeight = reader.ReadSingle();
            FalcoNumber = reader.ReadSingle();
            ShotSpeed = reader.ReadSingle();
            ShotInterval = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)StartMode);
            writer.Write((byte)EnemyType);
            writer.Write((byte)Weapon);
            writer.Write((byte)Shield);
            writer.Write(ScopeRange);
            writer.Write(ScopeOffset);
            writer.Write(MovingRange);
            writer.Write(FallWarpHeight);
            writer.Write(FalcoNumber);
            writer.Write(ShotSpeed);
            writer.Write(ShotInterval);
        }
    }
}
