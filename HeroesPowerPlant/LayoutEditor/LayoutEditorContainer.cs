using System.Windows.Forms;

namespace HeroesPowerPlant.LayoutEditor
{
    public partial class LayoutEditorContainer : Form
    {
        public LayoutEditorContainer()
        {
            InitializeComponent();

            tabControlLayoutEditor.TabPages.Add(new LayoutEditor());
        }
    }
}
