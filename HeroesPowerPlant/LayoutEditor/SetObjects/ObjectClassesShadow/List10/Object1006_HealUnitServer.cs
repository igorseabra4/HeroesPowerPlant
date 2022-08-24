namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1006_HealUnitServer : SetObjectShadow
    {
        public enum EServe : int
        {
            Bomb,
            HealUnit
        }

        //BombServer(Type)
        [MiscSetting]
        public EServe Serve { get; set; }
    }
}
