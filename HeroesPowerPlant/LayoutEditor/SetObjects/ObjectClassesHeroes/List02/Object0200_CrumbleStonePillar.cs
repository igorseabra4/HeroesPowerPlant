namespace HeroesPowerPlant.LayoutEditor
{
    public enum CrumbleStonePillarType : byte
    {
        Left = 0,
        Right = 1,
        Center = 2
    }

    public class Object0200_CrumbleStonePillar : SetObjectHeroes
    {
        public CrumbleStonePillarType RuinType
        {
            get => (CrumbleStonePillarType)ReadByte(4);
            set => Write(4, (byte)value);
        }
    }
}
