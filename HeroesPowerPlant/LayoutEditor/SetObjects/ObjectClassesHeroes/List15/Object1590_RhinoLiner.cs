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

        [MiscSetting]
        public EEnemyType EnemyType { get; set; }
        [MiscSetting]
        public EPathType PathType { get; set; }
        [MiscSetting]
        public EIronBallType IronBallType { get; set; }
        [MiscSetting]
        public float WeaponSpeed { get; set; }
        [MiscSetting]
        public int AttackInterval { get; set; }
        [MiscSetting]
        public float MoveSpeedMin { get; set; }
        [MiscSetting]
        public float MoveSpeed { get; set; }
        [MiscSetting]
        public float MoveSpeedMax { get; set; }
    }
}
