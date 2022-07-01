namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1005_Researcher : SetObjectShadow
    {
        //Researcher
        public ResearcherType PositionType
        {
            get => (ResearcherType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum ResearcherType
    {
        FaceUp,
        FaceDown
    }
}
