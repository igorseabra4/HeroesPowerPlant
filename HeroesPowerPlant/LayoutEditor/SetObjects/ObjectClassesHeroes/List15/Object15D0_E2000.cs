namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15D0_E2000 : SetObjectHeroes
    {
        public enum EEnemyType : byte
        {
            Normal = 0,
            Special = 1
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
        public int AttackInterval { get; set; }
        [MiscSetting]
        public int AttackFrame { get; set; }
        [MiscSetting]
        public float Distance { get; set; }
    }
}
