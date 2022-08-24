namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0006_SwitchPP : SetObjectHeroes
    {
        public enum EMode : byte
        {
            Push = 0,
            Pull = 1
        }

        [MiscSetting]
        public EMode SwitchMode { get; set; }
    }
}