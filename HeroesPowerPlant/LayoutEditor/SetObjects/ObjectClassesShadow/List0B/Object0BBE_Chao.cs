namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BBE_Chao : SetObjectShadow
    {
        public enum EChao : int
        {
            Normal = 0x00, //CHAO
            Cheese = 0x01 //CHEEZ
        }

        [MiscSetting]
        public EChao Chao { get; set; }
        [MiscSetting]
        public float MoveRadius { get; set; }
        [MiscSetting]
        public float MoveSpeed { get; set; }
    }
}
