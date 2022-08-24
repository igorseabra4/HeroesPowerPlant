namespace HeroesPowerPlant.LayoutEditor
{
    public class Object106C_SkyRuinsJewel : SetObjectShadow
    {
        public enum EModel : int
        {
            Out,
            In
        }

        //PowerDeviceNaked
        [MiscSetting]
        public EModel Model { get; set; }
    }
}
