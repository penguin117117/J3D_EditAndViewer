using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using J3D_EditAndViewer.IO;
using J3D_EditAndViewer.FileFormat;

namespace J3D_EditAndViewer
{
    public partial class MainWindow : Form
    {
        private TreeNode _tn;
        private TreeView _tv;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            J3DFileDialog J3D_FileDialog = new J3DFileDialog(SceneTreeView);
            J3D_FileDialog.Open();
            _tn = J3D_FileDialog._tn;
            _tv = J3D_FileDialog.treeView;
        }

        private void SceneTreeView_Click(object sender, EventArgs e)
        {
            
        }

        private void SceneTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            
            
            
        }

        private void SceneTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            textBox1.Text = e.Node.ImageKey;
        }
    }
}
