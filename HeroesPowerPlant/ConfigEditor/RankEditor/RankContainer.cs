using Heroes.SDK.Definitions.Enums;
using Heroes.SDK.Definitions.Structures.Stage.Rank;

namespace HeroesPowerPlant.RankEditor
{
    public class RankContainer
    {
        public Stage stageID;
        public NormalMission normalMission;
        public ExtraMissionScore extraMissionScore;
        public ExtraMissionTime extraMissionTime;

        public RankContainer()
        {
            stageID = Stage.Null;
            normalMission = new NormalMission();
            extraMissionScore = new ExtraMissionScore();
            extraMissionTime = new ExtraMissionTime();
        }
    }
}
