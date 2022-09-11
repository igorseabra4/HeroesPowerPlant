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

        [MiscSetting]
        public EEnemyType EnemyType { get; set; }
        [MiscSetting]
        public EAppear Appear { get; set; }
        [MiscSetting]
        public float MoveRange { get; set; }
        [MiscSetting]
        public float ScopeRange { get; set; }
        [MiscSetting]
        public float ScopeOffset { get; set; }
        [MiscSetting]
        public short Unknown { get; set; }
        [MiscSetting]
        public short AttackInterval { get; set; }
        [MiscSetting]
        public float AttackSpeed { get; set; }
    }
}
