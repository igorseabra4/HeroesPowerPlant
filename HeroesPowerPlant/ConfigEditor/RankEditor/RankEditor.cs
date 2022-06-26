using HeroesPowerPlant.Shared.IO.Config;
using SonicHeroes.Utils.StageInjector.Common.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace HeroesPowerPlant.RankEditor
{
    public partial class RankEditor : Form
    {
        public RankEditor()
        {
            InitializeComponent();

            RankEditorNewConfig();
        }

        private void LayoutEditor_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }
        
        private string rankJsonPath => Path.Combine(Path.GetDirectoryName(Program.MainForm.ConfigEditor.GetOpenConfigFileName()), "RankReqs.json");
        
        internal void RankEditorNewConfig()
        {
            NumericSonicA.Value = 0;
            NumericSonicB.Value = 0;
            NumericSonicC.Value = 0;
            NumericSonicD.Value = 0;
            NumericDarkA.Value = 0;
            NumericDarkB.Value = 0;
            NumericDarkC.Value = 0;
            NumericDarkD.Value = 0;
            NumericRoseA.Value = 0;
            NumericRoseB.Value = 0;
            NumericRoseC.Value = 0;
            NumericRoseD.Value = 0;
            NumericChaotixA.Value = 0;
            NumericChaotixB.Value = 0;
            NumericChaotixC.Value = 0;
            NumericChaotixD.Value = 0;

            NumericSonicEA.Value = 0;
            NumericSonicEB.Value = 0;
            NumericSonicEC.Value = 0;
            NumericSonicED.Value = 0;
            NumericChaotixEA.Value = 0;
            NumericChaotixEB.Value = 0;
            NumericChaotixEC.Value = 0;
            NumericChaotixED.Value = 0;

            NumericDarkEAMin.Value = 0;
            NumericDarkEBMin.Value = 0;
            NumericDarkECMin.Value = 0;
            NumericDarkEDMin.Value = 0;
            NumericRoseEAMin.Value = 0;
            NumericRoseEBMin.Value = 0;
            NumericRoseECMin.Value = 0;
            NumericRoseEDMin.Value = 0;

            NumericDarkEASec.Value = 0;
            NumericDarkEBSec.Value = 0;
            NumericDarkECSec.Value = 0;
            NumericDarkEDSec.Value = 0;
            NumericRoseEASec.Value = 0;
            NumericRoseEBSec.Value = 0;
            NumericRoseECSec.Value = 0;
            NumericRoseEDSec.Value = 0;
        }

        public void RankEditorOpenConfig()
        {
            if (File.Exists(rankJsonPath))
            {
                RankContainer container = JsonSerializable<RankContainer>.FromPath(rankJsonPath);
                NumericSonicB.Value = container.normalMission.Sonic.B;
                NumericSonicC.Value = container.normalMission.Sonic.C;
                NumericSonicD.Value = container.normalMission.Sonic.D;
                NumericSonicA.Value = container.normalMission.Sonic.A;
                NumericDarkA.Value = container.normalMission.Dark.A;
                NumericDarkB.Value = container.normalMission.Dark.B;
                NumericDarkC.Value = container.normalMission.Dark.C;
                NumericDarkD.Value = container.normalMission.Dark.D;
                NumericRoseA.Value = container.normalMission.Rose.A;
                NumericRoseB.Value = container.normalMission.Rose.B;
                NumericRoseC.Value = container.normalMission.Rose.C;
                NumericRoseD.Value = container.normalMission.Rose.D;
                NumericChaotixA.Value = container.normalMission.Chaotix.A;
                NumericChaotixB.Value = container.normalMission.Chaotix.B;
                NumericChaotixC.Value = container.normalMission.Chaotix.C;
                NumericChaotixD.Value = container.normalMission.Chaotix.D;

                NumericSonicEA.Value = container.extraMissionScore.Sonic.A;
                NumericSonicEB.Value = container.extraMissionScore.Sonic.B;
                NumericSonicEC.Value = container.extraMissionScore.Sonic.C;
                NumericSonicED.Value = container.extraMissionScore.Sonic.D;
                NumericChaotixEA.Value = container.extraMissionScore.Chaotix.A;
                NumericChaotixEB.Value = container.extraMissionScore.Chaotix.B;
                NumericChaotixEC.Value = container.extraMissionScore.Chaotix.C;
                NumericChaotixED.Value = container.extraMissionScore.Chaotix.D;

                NumericDarkEAMin.Value = container.extraMissionTime.Dark.RankA.Min;
                NumericDarkEBMin.Value = container.extraMissionTime.Dark.RankB.Min;
                NumericDarkECMin.Value = container.extraMissionTime.Dark.RankC.Min;
                NumericDarkEDMin.Value = container.extraMissionTime.Dark.RankD.Min;
                NumericRoseEAMin.Value = container.extraMissionTime.Rose.RankA.Min;
                NumericRoseEBMin.Value = container.extraMissionTime.Rose.RankB.Min;
                NumericRoseECMin.Value = container.extraMissionTime.Rose.RankC.Min;
                NumericRoseEDMin.Value = container.extraMissionTime.Rose.RankD.Min;

                NumericDarkEASec.Value = container.extraMissionTime.Dark.RankA.Sec;
                NumericDarkEBSec.Value = container.extraMissionTime.Dark.RankB.Sec;
                NumericDarkECSec.Value = container.extraMissionTime.Dark.RankC.Sec;
                NumericDarkEDSec.Value = container.extraMissionTime.Dark.RankD.Sec;
                NumericRoseEASec.Value = container.extraMissionTime.Rose.RankA.Sec;
                NumericRoseEBSec.Value = container.extraMissionTime.Rose.RankB.Sec;
                NumericRoseECSec.Value = container.extraMissionTime.Rose.RankC.Sec;
                NumericRoseEDSec.Value = container.extraMissionTime.Rose.RankD.Sec;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }

        private void SaveToFile()
        {
            RankContainer container = new RankContainer();

            container.stageID = Program.MainForm.ConfigEditor.currentID;

            container.normalMission.SetAllTeamRankScores(
                (ushort)NumericSonicA.Value, (ushort)NumericSonicB.Value, (ushort)NumericSonicC.Value, (ushort)NumericSonicD.Value,
                (ushort)NumericDarkA.Value, (ushort)NumericDarkB.Value, (ushort)NumericDarkC.Value, (ushort)NumericDarkD.Value,
                (ushort)NumericRoseA.Value, (ushort)NumericRoseB.Value, (ushort)NumericRoseC.Value, (ushort)NumericRoseD.Value,
                (ushort)NumericChaotixA.Value, (ushort)NumericChaotixB.Value, (ushort)NumericChaotixC.Value, (ushort)NumericChaotixD.Value
            );

            container.extraMissionScore.SetTeamSonicRankScores((ushort)NumericSonicEA.Value, (ushort)NumericSonicEB.Value, (ushort)NumericSonicEC.Value, (ushort)NumericSonicED.Value);
            container.extraMissionScore.SetTeamChaotixRankScores((ushort)NumericChaotixEA.Value, (ushort)NumericChaotixEB.Value, (ushort)NumericChaotixEC.Value, (ushort)NumericChaotixED.Value);

            container.extraMissionTime.SetDarkRankTime((byte)NumericDarkEAMin.Value, (byte)NumericDarkEASec.Value,
                (byte)NumericDarkEBMin.Value, (byte)NumericDarkEBSec.Value,
                (byte)NumericDarkECMin.Value, (byte)NumericDarkECSec.Value,
                (byte)NumericDarkEDMin.Value, (byte)NumericDarkEDSec.Value
            );

            container.extraMissionTime.SetRoseRankTime((byte)NumericRoseEAMin.Value, (byte)NumericRoseEASec.Value,
                (byte)NumericRoseEBMin.Value, (byte)NumericRoseEBSec.Value,
                (byte)NumericRoseECMin.Value, (byte)NumericRoseECSec.Value,
                (byte)NumericRoseEDMin.Value, (byte)NumericRoseEDSec.Value
            );

            JsonSerializable<RankContainer>.ToPath(container, rankJsonPath);
        }
    }
}