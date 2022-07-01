namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07E8_ElecRollHexa : SetObjectShadow
    {
        //ElecRollHexa
        public float RotateSpeed
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
    }
}
