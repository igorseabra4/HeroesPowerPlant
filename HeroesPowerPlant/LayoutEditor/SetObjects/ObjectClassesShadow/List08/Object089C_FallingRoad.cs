namespace HeroesPowerPlant.LayoutEditor
{
    public class Object089C_FallingRoad : SetObjectShadow
    {
        //FallRoad
        public float Height
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
    }
}
