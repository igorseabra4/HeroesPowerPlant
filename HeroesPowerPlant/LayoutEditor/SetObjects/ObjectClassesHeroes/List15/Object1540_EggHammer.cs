namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1540_EggHammer : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Normal = 0,
            Helmet = 1
        }

        [MiscSetting]
        public EEnemyType EnemyType { get; set; }
        [MiscSetting]
        public EAppear Appear { get; set; }
        [MiscSetting]
        public float MoveSpeed { get; set; }
        [MiscSetting]
        public float MoveRange { get; set; }
        [MiscSetting]
        public float ScopeRange { get; set; }
        [MiscSetting]
        public float ScopeOffset { get; set; }
        [MiscSetting]
        public int AttackInterval { get; set; }
        [MiscSetting]
        public float WeaponSpeed { get; set; }
        [MiscSetting]
        public float FallDistance { get; set; }
    }
}
