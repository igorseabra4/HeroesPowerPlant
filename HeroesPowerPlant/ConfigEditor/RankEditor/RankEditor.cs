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
            TopMost = true;
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

                NumericSonicA.Value = container.normalMission.SonicA;
                NumericSonicB.Value = container.normalMission.SonicB;
                NumericSonicC.Value = container.normalMission.SonicC;
                NumericSonicD.Value = container.normalMission.SonicD;
                NumericDarkA.Value = container.normalMission.DarkA;
                NumericDarkB.Value = container.normalMission.DarkB;
                NumericDarkC.Value = container.normalMission.DarkC;
                NumericDarkD.Value = container.normalMission.DarkD;
                NumericRoseA.Value = container.normalMission.RoseA;
                NumericRoseB.Value = container.normalMission.RoseB;
                NumericRoseC.Value = container.normalMission.RoseC;
                NumericRoseD.Value = container.normalMission.RoseD;
                NumericChaotixA.Value = container.normalMission.ChaotixA;
                NumericChaotixB.Value = container.normalMission.ChaotixB;
                NumericChaotixC.Value = container.normalMission.ChaotixC;
                NumericChaotixD.Value = container.normalMission.ChaotixD;

                NumericSonicEA.Value = container.extraMissionScore.SonicA;
                NumericSonicEB.Value = container.extraMissionScore.SonicB;
                NumericSonicEC.Value = container.extraMissionScore.SonicC;
                NumericSonicED.Value = container.extraMissionScore.SonicD;
                NumericChaotixEA.Value = container.extraMissionScore.ChaotixA;
                NumericChaotixEB.Value = container.extraMissionScore.ChaotixB;
                NumericChaotixEC.Value = container.extraMissionScore.ChaotixC;
                NumericChaotixED.Value = container.extraMissionScore.ChaotixD;

                NumericDarkEAMin.Value = container.extraMissionTime.DarkAMin;
                NumericDarkEBMin.Value = container.extraMissionTime.DarkBMin;
                NumericDarkECMin.Value = container.extraMissionTime.DarkCMin;
                NumericDarkEDMin.Value = container.extraMissionTime.DarkDMin;
                NumericRoseEAMin.Value = container.extraMissionTime.RoseAMin;
                NumericRoseEBMin.Value = container.extraMissionTime.RoseBMin;
                NumericRoseECMin.Value = container.extraMissionTime.RoseCMin;
                NumericRoseEDMin.Value = container.extraMissionTime.RoseDMin;

                NumericDarkEASec.Value = container.extraMissionTime.DarkASec;
                NumericDarkEBSec.Value = container.extraMissionTime.DarkBSec;
                NumericDarkECSec.Value = container.extraMissionTime.DarkCSec;
                NumericDarkEDSec.Value = container.extraMissionTime.DarkDSec;
                NumericRoseEASec.Value = container.extraMissionTime.RoseASec;
                NumericRoseEBSec.Value = container.extraMissionTime.RoseBSec;
                NumericRoseECSec.Value = container.extraMissionTime.RoseCSec;
                NumericRoseEDSec.Value = container.extraMissionTime.RoseDSec;
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

            container.normalMission.SonicA = (ushort)NumericSonicA.Value;
            container.normalMission.SonicB = (ushort)NumericSonicB.Value;
            container.normalMission.SonicC = (ushort)NumericSonicC.Value;
            container.normalMission.SonicD = (ushort)NumericSonicD.Value;
            container.normalMission.DarkA = (ushort)NumericDarkA.Value;
            container.normalMission.DarkB = (ushort)NumericDarkB.Value;
            container.normalMission.DarkC = (ushort)NumericDarkC.Value;
            container.normalMission.DarkD = (ushort)NumericDarkD.Value;
            container.normalMission.RoseA = (ushort)NumericRoseA.Value;
            container.normalMission.RoseB = (ushort)NumericRoseB.Value;
            container.normalMission.RoseC = (ushort)NumericRoseC.Value;
            container.normalMission.RoseD = (ushort)NumericRoseD.Value;
            container.normalMission.ChaotixA = (ushort)NumericChaotixA.Value;
            container.normalMission.ChaotixB = (ushort)NumericChaotixB.Value;
            container.normalMission.ChaotixC = (ushort)NumericChaotixC.Value;
            container.normalMission.ChaotixD = (ushort)NumericChaotixD.Value;

            container.extraMissionScore.SonicA = (ushort)NumericSonicEA.Value;
            container.extraMissionScore.SonicB = (ushort)NumericSonicEB.Value;
            container.extraMissionScore.SonicC = (ushort)NumericSonicEC.Value;
            container.extraMissionScore.SonicD = (ushort)NumericSonicED.Value;
            container.extraMissionScore.ChaotixA = (ushort)NumericChaotixEA.Value;
            container.extraMissionScore.ChaotixB = (ushort)NumericChaotixEB.Value;
            container.extraMissionScore.ChaotixC = (ushort)NumericChaotixEC.Value;
            container.extraMissionScore.ChaotixD = (ushort)NumericChaotixED.Value;

            container.extraMissionTime.DarkAMin = (byte)NumericDarkEAMin.Value;
            container.extraMissionTime.DarkBMin = (byte)NumericDarkEBMin.Value;
            container.extraMissionTime.DarkCMin = (byte)NumericDarkECMin.Value;
            container.extraMissionTime.DarkDMin = (byte)NumericDarkEDMin.Value;
            container.extraMissionTime.RoseAMin = (byte)NumericRoseEAMin.Value;
            container.extraMissionTime.RoseBMin = (byte)NumericRoseEBMin.Value;
            container.extraMissionTime.RoseCMin = (byte)NumericRoseECMin.Value;
            container.extraMissionTime.RoseDMin = (byte)NumericRoseEDMin.Value;

            container.extraMissionTime.DarkASec = (byte)NumericDarkEASec.Value;
            container.extraMissionTime.DarkBSec = (byte)NumericDarkEBSec.Value;
            container.extraMissionTime.DarkCSec = (byte)NumericDarkECSec.Value;
            container.extraMissionTime.DarkDSec = (byte)NumericDarkEDSec.Value;
            container.extraMissionTime.RoseASec = (byte)NumericRoseEASec.Value;
            container.extraMissionTime.RoseBSec = (byte)NumericRoseEBSec.Value;
            container.extraMissionTime.RoseCSec = (byte)NumericRoseECSec.Value;
            container.extraMissionTime.RoseDSec = (byte)NumericRoseEDSec.Value;

            JsonSerializable<RankContainer>.ToPath(container, rankJsonPath);
        }
    }
}