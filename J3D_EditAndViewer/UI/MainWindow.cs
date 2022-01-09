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
using J3D_EditAndViewer.UI.MainWindowSys;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace J3D_EditAndViewer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            J3DFileDialog J3D_FileDialog = new J3DFileDialog();
            J3D_FileDialog.Open();
            SceneTreeNodeView sceneTreeNodeView = new SceneTreeNodeView(SceneTreeView,J3D_FileDialog.J3DData);
            sceneTreeNodeView.SetTree();
        }

        private void SceneTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            textBox1.Text = e.Node.ImageKey;
        }

        private void GL_Panel_Resize(object sender, EventArgs e)
        {
            //Console.WriteLine("Autosize");
            //glControl.Width = GL_Panel.Width;
            //glControl.Height = GL_Panel.Height;
            //glControl.SwapBuffers();
            //glControl.Refresh();
            Console.WriteLine(GL_Panel.Width);
            
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(glControl.BackColor);
            SetProjection();
            Matrix4 look = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref look);
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            SetProjection();
            glControl.SwapBuffers();
            glControl.Refresh();
        }

        private void SetProjection()
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            float h = 4.0f, w = h * glControl.AspectRatio;
            Matrix4 proj = Matrix4.CreateOrthographic(w, h, 0.01f, 2.0f);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
        }

    }
}
