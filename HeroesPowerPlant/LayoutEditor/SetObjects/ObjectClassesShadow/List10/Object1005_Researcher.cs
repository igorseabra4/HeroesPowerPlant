namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1005_Researcher : SetObjectShadow
    {
        public enum EResearcherType : int
        {
            FaceUp,
            FaceDown
        }
        //Researcher
        [MiscSetting]
        public EResearcherType ResearcherType { get; set; }
    }
}
