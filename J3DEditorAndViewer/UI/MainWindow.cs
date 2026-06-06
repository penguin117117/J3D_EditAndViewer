// OpenTK
using OpenTK.GLControl;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.IO;
using J3DEditorAndViewer.UI.MainWindowSys;
using J3DEditorAndViewer.UI.Renderer;

namespace J3DEditorAndViewer
{
    public partial class MainWindow : Form
    {
        IRenderer renderer;

        public MainWindow()
        {
            InitializeComponent();
            //glControl.Dock = DockStyle.Fill;  // GLControlをフォーム全体に埋め込む
            //glControl.Load += glControl_Load;
            //glControl.Resize += glControl_Resize;
            //glControl.Paint += glControl_Paint;

            // ボタンを作成してクリックイベントを追加
            //button1 = new Button();
            button1.Text = "描画開始";
            //button1.Dock = DockStyle.Top;
            button1.Click += button1_Click;

            //this.Controls.Add(glControl);
            //this.Controls.Add(button1);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            J3DFileDialog J3D_FileDialog = new J3DFileDialog();
            J3D_FileDialog.Open();
            SceneTreeNodeView sceneTreeNodeView = new SceneTreeNodeView(SceneTreeView, J3D_FileDialog.J3DData);
            sceneTreeNodeView.SetTree();

            renderer?.Dispose();

            renderer = new OpenGLv4(J3D_FileDialog, glControl.Width, glControl.Height);
            IsModelLoad = true;

            renderer.Update(glControl);
        }

        private void SceneTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            textBox1.Text = e.Node.ImageKey;
        }

        private void GL_Panel_Resize(object sender, EventArgs e)
        {

        }


        private bool IsModelLoad = false;
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            renderer?.Update(glControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            renderer?.Update(glControl);
        }

        // private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);  // カメラの初期位置
        // private Vector2 cameraRotation = new Vector2(0.0f, 0.0f);  // カメラの初期回転
        // private float cameraDistance = 3.0f;  // カメラの回転軸に対する位置
        private float cameraSpeed = 0.25f;  // カメラの移動速度

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (renderer is null) { return; }

            if (e.KeyCode == Keys.W)
            {   // 前に移動
                renderer.CameraPosition.X -= cameraSpeed * (float)Math.Sin(renderer.CameraAngle.Y);
                renderer.CameraPosition.Z -= cameraSpeed * (float)Math.Cos(renderer.CameraAngle.Y);
            }
            if (e.KeyCode == Keys.S)
            {   // 後ろに移動
                renderer.CameraPosition.X += cameraSpeed * (float)Math.Sin(renderer.CameraAngle.Y);
                renderer.CameraPosition.Z += cameraSpeed * (float)Math.Cos(renderer.CameraAngle.Y);
            }
            if (e.KeyCode == Keys.A)
            {   // 左に移動
                renderer.CameraPosition.Z += cameraSpeed * (float)Math.Sin(renderer.CameraAngle.Y);
                renderer.CameraPosition.X -= cameraSpeed * (float)Math.Cos(renderer.CameraAngle.Y);
            }
            if (e.KeyCode == Keys.D)
            {   // 右に移動
                renderer.CameraPosition.Z -= cameraSpeed * (float)Math.Sin(renderer.CameraAngle.Y);
                renderer.CameraPosition.X += cameraSpeed * (float)Math.Cos(renderer.CameraAngle.Y);
            }
            if (e.KeyCode == Keys.Q)
            {   // 上に移動
                renderer.CameraPosition.Y += cameraSpeed;
            }
            if (e.KeyCode == Keys.E)
            {   // 下に移動
                renderer.CameraPosition.Y -= cameraSpeed;
            }

            renderer.Update(glControl);
        }
        private Point BeforeMousePoint = new Point();
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (renderer is null) return;

            float deltaHorizon = e.X - BeforeMousePoint.X;
            float deltaVertical = e.Y - BeforeMousePoint.Y;
            BeforeMousePoint = new Point(e.X, e.Y);

            if (e.Button != MouseButtons.None)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:

                        deltaHorizon *= 0.005f;
                        deltaVertical *= 0.005f;

                        renderer.CameraAngle = Quaternion.FromEulerAngles(deltaVertical, deltaHorizon, 0.0f) * renderer.CameraAngle;

                        //const double halfPi = Math.PI * 0.5;
                        //if (Math.Abs(renderer.CameraAngle.X) > halfPi)
                        //{
                        //    renderer.CameraAngle.X = (float)halfPi * ((renderer.CameraAngle.X < 0) ? -0.999f : 0.999f);
                        //}

                        renderer.CameraAngle.X += deltaVertical;
                        renderer.CameraAngle.Y += deltaHorizon;
                        const double halfPi = Math.PI * 0.5;
                        if (Math.Abs(renderer.CameraAngle.X) > halfPi)
                        {
                            renderer.CameraAngle.X = (float)halfPi * ((renderer.CameraAngle.X < 0) ? -0.999f : 0.999f);
                        }
                        break;
                    case MouseButtons.Right:
                        break;
                }
            }
            renderer.Update(glControl);
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            if (renderer is null) return;
            renderer.Reset();
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            if (renderer is null) return;
            renderer.Resize(glControl.Width, glControl.Height);
            renderer.Update(glControl);
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            renderer.CameraDistance += -0.01f * e.Delta;
            renderer.Update(glControl);
        }


        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
