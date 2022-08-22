using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1005_Researcher : SetObjectShadow
    {

        public enum EResearcherType
        {
            FaceUp,
            FaceDown
        }
        //Researcher
        public EResearcherType ResearcherType { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            ResearcherType = (EResearcherType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)ResearcherType);
        }
    }
}
