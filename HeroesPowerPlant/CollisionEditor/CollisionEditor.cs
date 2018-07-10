using System;
using System.Windows.Forms;
using static HeroesPowerPlant.Collision.CollisionFunctions;

namespace HeroesPowerPlant.Collision
{
    public partial class CollisionEditor : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public CollisionEditor()
        {
            InitializeComponent();
        }

        private void CollisionEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        public string currentCLfileName;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenOBJFile = new OpenFileDialog
            {
                Filter = "OBJ Files|*.obj|All Files|*.*"
            };
            SaveFileDialog SaveCLFile = new SaveFileDialog
            {
                Filter = "CL Files|*.cl"
            };

            if (OpenOBJFile.ShowDialog() == DialogResult.OK)
                if (SaveCLFile.ShowDialog() == DialogResult.OK)
                {
                    currentCLfileName = SaveCLFile.FileName;
                    ConvertOBJtoCL(OpenOBJFile.FileName, currentCLfileName, (byte)numericDepthLevel.Value);
                    initFile();
                }
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenCLFile = new OpenFileDialog()
            {
                Filter = "CL Files|*.cl"
            };
            if (OpenCLFile.ShowDialog() == DialogResult.OK)
            {
                currentCLfileName = OpenCLFile.FileName;
                initFile();
            }
        }

        private void exportOBJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveOBJFile = new SaveFileDialog
            {
                Filter = "OBJ Files|*.obj|All Files|*.*"
            };
            if (SaveOBJFile.ShowDialog() == DialogResult.OK)
            {
                ConvertCLtoOBJ(SaveOBJFile.FileName);
                initFile();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "No CL loaded";
            currentCLfileName = null;
            UseHeader = new Header();
            CLVertexArray = null;
            CLTriangleArray = null;
            CLQuadNodeList = null;

            label3.Text = "Number of Vertices: ";
            label4.Text = "Number of Triangles: ";
            label5.Text = "Number of QuadNodes: ";

            exportOBJToolStripMenuItem.Enabled = false;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (currentCLfileName != null)
            {
                OpenFileDialog OpenOBJFile = new OpenFileDialog();
                OpenOBJFile.Filter = "OBJ Files|*.obj|All Files|*.*";
                if (OpenOBJFile.ShowDialog() == DialogResult.OK)
                {
                    ConvertOBJtoCL(OpenOBJFile.FileName, currentCLfileName, (byte)numericDepthLevel.Value);
                    initFile();
                }
            }
            else
            {
                newToolStripMenuItem_Click(sender, e);
            }
        }

        public void initFile()
        {
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Step = 1;

            if (LoadCLFile(currentCLfileName))
            {
                label1.Text = "Loaded " + currentCLfileName;
                label3.Text = "Number of Vertices: " + UseHeader.numVertices.ToString();
                label4.Text = "Number of Triangles: " + UseHeader.numTriangles.ToString();
                label5.Text = "Number of QuadNodes: " + UseHeader.numQuadnodes.ToString();

                exportOBJToolStripMenuItem.Enabled = true;
            }

            progressBar1.Value = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                numericDepthLevel.Enabled = false;
            else
                numericDepthLevel.Enabled = true;
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It's recommended to set the depth level to Auto (so it will be as big as possible) and the power flag to D. However, custom collision files might make the game crash in random spots, specially if your depth level ends up at 8 or 9. If that's happening, set the depth level to 5.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetermineRenderStuff2();
        }
    }
}