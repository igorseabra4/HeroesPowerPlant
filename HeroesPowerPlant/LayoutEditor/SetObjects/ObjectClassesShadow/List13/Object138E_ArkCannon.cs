namespace HeroesPowerPlant.LayoutEditor
{
    public class Object138E_ArkCannon : SetObjectShadow
    {
        public enum EModel : int
        {
            Ground, //GRAND
            Air // AIR
        }

        [MiscSetting]
        public EModel Model { get; set; }
        [MiscSetting]
        public float DetectRange { get; set; }
    }
}

