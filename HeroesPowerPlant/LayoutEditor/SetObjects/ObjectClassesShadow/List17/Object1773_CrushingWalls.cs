namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1773_CrushingWalls : SetObjectShadow
    {
        //SetCloseWall
        public float WallStartAdditionalSize
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float CloseRate
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}
