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
//using System.Collections.
using OpenTK;
using OpenTK.Graphics;
//using OpenTK;

namespace J3D_EditAndViewer
{
    public partial class MainWindow : Form
    {
        private int vtxcount;
        private List<Vector3> vertexPosition;
        private List<Vector3> vertexNormal;
        private static float _farRange = 64.0f;

        public float FarRange
        {
            get => _farRange;
            private set
            {
                if (value < float.MaxValue && value > float.MaxValue) _farRange = value;
                if (value > float.MaxValue) _farRange = float.MaxValue;
                if (value < float.MinValue) _farRange = float.MinValue;


            }
        }

        public MainWindow()
        {
            InitializeComponent();
            glControl.Load += delegate { initGLControl(); };
            glControl.Resize += delegate { initGLControl(); };
            glControl.Paint += delegate { initGLControl(); };
        }


        void initGLControl()
        {
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);
            GL.Viewport(0, 0, glControl.Size.Width, glControl.Size.Height);


            //透視射影
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4, 
                (float)glControl.Size.Width / (float)glControl.Size.Height,
                0.1f, 
                FarRange
                );
            GL.LoadMatrix(ref projection);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 modelview = Matrix4.LookAt(Vector3.UnitZ * 2, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref modelview);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            J3DFileDialog J3D_FileDialog = new J3DFileDialog();
            J3D_FileDialog.Open();
            SceneTreeNodeView sceneTreeNodeView = new SceneTreeNodeView(SceneTreeView, J3D_FileDialog.J3DData);
            sceneTreeNodeView.SetTree();

            //vertexPosition = new List<Vector3>();
            //vertexNormal = new List<Vector3>();

            //vertexPosition = J3D_FileDialog.J3DData.Model.VerTexData.Position;
            //vertexNormal = J3D_FileDialog.J3DData.Model.VerTexData.Normal;

            //Console.WriteLine($"PositionCount: {vertexPosition.Count}");
            //Console.WriteLine($"NormalCount: {vertexNormal.Count}");
            //IsModelLoad = true;
        }

        private void SceneTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            textBox1.Text = e.Node.ImageKey;
        }

        private void GL_Panel_Resize(object sender, EventArgs e)
        {
            
        }

        private void glControl_Load(object sender, EventArgs e)
        {

        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            SetProjection();
            glControl.SwapBuffers();
            glControl.Refresh();
        }

        private Vector3 m_CamTarget = new Vector3(0f, 0f, 0f);
        private Vector2 m_CamRotation = new Vector2(1f,1f);
        private Vector3 m_CamPosition = new Vector3(1f, 1f, 1f);
        //private Vector3 m_CamRotation = new Vector3(0f,0f,0f);
        private float m_CamDistance = 0.2f;
        private bool m_UpsideDown;
        private Matrix4 m_CamMatrix, m_SkyboxMatrix;
        private bool IsModelLoad = false;

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            //FarRange += 0.1f * e.Delta*10000;
            
            //Vector3 up;

            //if (Math.Cos(m_CamRotation.Y) < 0)
            //{
            //    m_UpsideDown = true;
            //    up = new Vector3(0.0f, -1.0f, 0.0f);
            //}
            //else
            //{
            //    m_UpsideDown = false;
            //    up = new Vector3(0.0f, 1.0f, 0.0f);
            //}

            //m_CamPosition.X = m_CamDistance * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            //m_CamPosition.Y = m_CamDistance * (float)Math.Sin(m_CamRotation.Y);
            //m_CamPosition.Z = m_CamDistance * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            //Console.WriteLine(m_CamPosition);

            //Vector3 skybox_target;
            //skybox_target.X = -(float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            //skybox_target.Y = -(float)Math.Sin(m_CamRotation.Y);
            //skybox_target.Z = -(float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            //Vector3.Add(ref m_CamPosition, ref m_CamTarget, out m_CamPosition);

            //m_CamMatrix = Matrix4.LookAt(m_CamPosition, m_CamTarget, up);
            //m_SkyboxMatrix = Matrix4.LookAt(Vector3.Zero, skybox_target, up);
            //m_CamMatrix = Matrix4.Mult(Matrix4.CreateScale(0.0001f), m_CamMatrix);


            //float delta = -((e.Delta /*/ 120f*/) * 100000.0f);
            //m_CamTarget.X += delta * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            //m_CamTarget.Y += delta * (float)Math.Sin(m_CamRotation.Y);
            //m_CamTarget.Z += delta * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);


            ////UpdateCamera();
            ////SetProjection();
            //var proj = Matrix4.LookAt(7.0f, 5.0f, 3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            //GL.LoadMatrix(ref proj);
            //GL.MatrixMode(MatrixMode.Modelview);
            //glControl.SwapBuffers();
            //glControl.Refresh();



            ////////////float delta = ((e.Delta / 120f) * 100f);
            ////////////m_CamTarget.X += delta * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            ////////////m_CamTarget.Y += delta * (float)Math.Sin(m_CamRotation.Y);
            ////////////m_CamTarget.Z += delta * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            //////////////UpdateCamera();

            ////////////GL.MatrixMode(MatrixMode.Modelview);

            ////////////Vector3 up;

            ////////////if (Math.Cos(m_CamRotation.Y) < 0)
            ////////////{
            ////////////    m_UpsideDown = true;
            ////////////    up = new Vector3(0.0f, -1.0f, 0.0f);
            ////////////}
            ////////////else
            ////////////{
            ////////////    m_UpsideDown = false;
            ////////////    up = new Vector3(0.0f, 1.0f, 0.0f);
            ////////////}

            ////////////m_CamPosition.X += m_CamDistance * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            ////////////m_CamPosition.Y += m_CamDistance * (float)Math.Sin(m_CamRotation.Y);
            ////////////m_CamPosition.Z += m_CamDistance * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            ////////////Console.WriteLine(m_CamPosition);

            ////////////Vector3 skybox_target;
            ////////////skybox_target.X = -(float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            ////////////skybox_target.Y = -(float)Math.Sin(m_CamRotation.Y);
            ////////////skybox_target.Z = -(float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            //Vector3.Add(ref m_CamPosition, ref m_CamTarget, out m_CamPosition);

            ////////////m_CamMatrix = Matrix4.LookAt(m_CamPosition,m_CamTarget, up);
            //m_SkyboxMatrix = Matrix4.LookAt(Vector3.Zero, skybox_target, up);
            //m_CamMatrix = Matrix4.Mult(Matrix4.CreateScale(0.0001f), m_CamMatrix);

            ////////////GL.LoadMatrix(ref m_CamMatrix);
            //SetProjection();

            ////////////glControl.SwapBuffers();
            ////////////glControl.Refresh();




        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            if (!IsModelLoad) return;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);


            GL.PushMatrix();
            GL.Color3(0f,0f,1f);
            GL.Begin(PrimitiveType.TriangleFan);
            for (int i = 0; i< vertexPosition.Count; i++) 
            {
                
                GL.Vertex3(vertexPosition[i]/100000);
                //GL.Normal3(vertexNormal[i] / 100000);

            }
            //foreach (var position in vertexPosition)
            //{
            //    GL.Vertex3(position/100000);
            //    GL.Normal3();
            //}
            GL.End();
            GL.PushMatrix();

            initGLControl();

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
