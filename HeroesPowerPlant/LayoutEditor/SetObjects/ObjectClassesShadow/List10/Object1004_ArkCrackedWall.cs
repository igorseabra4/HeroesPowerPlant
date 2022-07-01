namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1004_ArkCrackedWall : SetObjectShadow
    {
        //BombingWall(Type{Out,In},Range point)
        public BombingWallType WallSide
        {
            get => (BombingWallType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float Range
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
    public enum BombingWallType
    {
        Out,
        In
    }
}
