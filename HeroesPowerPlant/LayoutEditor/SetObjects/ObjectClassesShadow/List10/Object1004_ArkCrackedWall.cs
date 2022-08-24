namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1004_ArkCrackedWall : SetObjectShadow
    {
        public enum EWallType : int
        {
            Out,
            In
        }

        //BombingWall(Type{Out,In},Range point)
        [MiscSetting]
        public EWallType WallType { get; set; }
        [MiscSetting]
        public float Range { get; set; }
    }
}
