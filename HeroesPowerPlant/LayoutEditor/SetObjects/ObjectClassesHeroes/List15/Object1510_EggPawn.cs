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

        [MiscSetting]
        public EStartMode StartMode { get; set; }
        [MiscSetting]
        public EEnemyType EnemyType { get; set; }
        [MiscSetting]
        public EWeapon Weapon { get; set; }
        [MiscSetting]
        public EShield Shield { get; set; }
        [MiscSetting]
        public short ScopeRange { get; set; }
        [MiscSetting]
        public short ScopeOffset { get; set; }
        [MiscSetting]
        public float MovingRange { get; set; }
        [MiscSetting]
        public float FallWarpHeight { get; set; }
        [MiscSetting]
        public float FalcoNumber { get; set; }
        [MiscSetting]
        public float ShotSpeed { get; set; }
        [MiscSetting]
        public int ShotInterval { get; set; }
    }
}
