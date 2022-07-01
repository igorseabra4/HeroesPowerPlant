namespace HeroesPowerPlant.LayoutEditor
{
    public class Object106C_SkyRuinsJewel : SetObjectShadow
    {
        //PowerDeviceNaked
        public PowerDeviceNakedModel Model
        {
            get => (PowerDeviceNakedModel)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum PowerDeviceNakedModel
    {
        Out,
        In
    }
}
