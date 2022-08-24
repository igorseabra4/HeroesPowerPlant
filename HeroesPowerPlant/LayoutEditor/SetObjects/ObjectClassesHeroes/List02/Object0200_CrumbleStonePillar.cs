namespace HeroesPowerPlant.LayoutEditor
{

    public class Object0200_CrumbleStonePillar : SetObjectHeroes
    {
        public enum EPillarType : byte
        {
            Left = 0,
            Right = 1,
            Center = 2
        }

        [MiscSetting]
        public EPillarType PillarType { get; set; }
    }
}
