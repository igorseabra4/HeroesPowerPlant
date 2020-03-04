using Heroes.SDK.Definitions.Structures.Stage.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heroes.SDK.Definitions.Enums;

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
