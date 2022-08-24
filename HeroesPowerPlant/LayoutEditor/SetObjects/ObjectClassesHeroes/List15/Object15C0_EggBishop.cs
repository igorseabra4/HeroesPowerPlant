namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15C0_EggBishop : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Bishop = 0,
            Magician = 1
        }

        [MiscSetting]
        public EEnemyType EnemyType { get; set; }
        [MiscSetting]
        public float MoveRange { get; set; }
        [MiscSetting]
        public float ScopeRange { get; set; }
        [MiscSetting]
        public float ScopeOffset { get; set; }
        [MiscSetting]
        public int AttackInterval { get; set; }
        [MiscSetting]
        public float MoveSpeed { get; set; }
    }
}
