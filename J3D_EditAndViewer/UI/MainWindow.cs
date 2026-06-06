using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using J3DEditorAndViewer.IO;
using J3DEditorAndViewer.FileFormat;
using J3DEditorAndViewer.UI.MainWindowSys;
using OpenTK.Graphics.OpenGL;
//using System.Collections.
using OpenTK;
using OpenTK.Graphics;
using System.Diagnostics;
//using OpenTK;

namespace J3DEditorAndViewer
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

            vertexPosition = new List<Vector3>();
            vertexNormal = new List<Vector3>();

            vertexPosition = J3D_FileDialog.J3DData.Model.VerTexData.Position;
            vertexNormal = J3D_FileDialog.J3DData.Model.VerTexData.Normal;

            Console.WriteLine($"PositionCount: {vertexPosition.Count}");
            Console.WriteLine($"NormalCount: {vertexNormal.Count}");
            IsModelLoad = true;
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
            if (!IsModelLoad) return;

            GL.Clear(ClearBufferMask.ColorBufferBit);  // 画面をクリア
            GL.LoadIdentity();

            // カメラの視点を設定（カメラの位置を変更）
            SetCameraPosition(cameraPosition, cameraRotation, cameraDistance);

            // 1. トライアングル面を描画
            DrawTriangles(vertexPosition.ToArray()/*, vertexNormal.ToArray()*/);



            // 2. 扇状（三角形扇）を描画
            //DrawFan();

            // バッファの交換
            glControl.SwapBuffers();

            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //GL.MatrixMode(MatrixMode.Modelview);


            //GL.PushMatrix();
            //GL.Color3(0f,0f,1f);
            //GL.Begin(PrimitiveType.TriangleFan);
            //for (int i = 0; i< vertexPosition.Count; i++) 
            //{

            //    GL.Vertex3(vertexPosition[i]/100000);
            //    //GL.Normal3(vertexNormal[i] / 100000);

            //}
            ////foreach (var position in vertexPosition)
            ////{
            ////    GL.Vertex3(position/100000);
            ////    GL.Normal3();
            ////}
            //GL.End();
            //GL.PushMatrix();

            //initGLControl();

            //glControl.SwapBuffers();
            //glControl.Refresh();
        }

        // 三角形面データ（トライアングル）
        // 各三角形の頂点は Vector3 配列に格納
        private Vector3[] triangleVertices = new Vector3[]
        {
            // 1つ目の三角形
            new Vector3(0.0f,  0.8f, 0.0f),   // 頂点1
            new Vector3(-0.8f, -0.8f, 0.0f),  // 頂点2
            new Vector3(0.8f, -0.8f, 0.0f),   // 頂点3

            // 2つ目の三角形
            new Vector3(0.0f,  0.8f, 0.0f),   // 頂点1
            new Vector3(-0.8f, -0.8f, 0.0f),  // 頂点2
            new Vector3(0.0f, -1.0f, 0.0f),   // 頂点3

            // 3つ目の三角形
            new Vector3(0.0f,  0.8f, 0.0f),   // 頂点1
            new Vector3(0.8f, -0.8f, 0.0f),   // 頂点2
            new Vector3(0.0f, -1.0f, 0.0f)    // 頂点3
        };

        // カメラ位置を設定するメソッド
        private void SetCameraPosition(Vector3 Position, Vector2 Axsis2, float Distance)
        {
            Vector3 cameraPos = new Vector3(Position);
            cameraPos.X -= Distance * ((float)Math.Cos(Axsis2.X) * (float)Math.Cos(Axsis2.Y));
            cameraPos.Y -= Distance * ((float)Math.Sin(Axsis2.Y));
            cameraPos.Z -= Distance * (-(float)Math.Sin(Axsis2.X) * (float)Math.Cos(Axsis2.Y));

            // カメラの位置を計算して、視点行列を設定
            Matrix4 cameraMatrix = Matrix4.LookAt(cameraPos, Position, Vector3.UnitY);
            GL.LoadMatrix(ref cameraMatrix);  // 行列を適用
        }

        private void DrawTriangles(Vector3[] triangleVertices)
        {
            // 頂点バッファを生成してデータを転送
            int vertexBuffer;
            GL.GenBuffers(1, out vertexBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

            // Vector3 配列から float[] 配列に変換
            float[] vertices = new float[triangleVertices.Length * 3];
            for (int i = 0; i < triangleVertices.Length; i++)
            {
                vertices[i * 3 + 0] = triangleVertices[i].X / 100f;
                vertices[i * 3 + 1] = triangleVertices[i].Y / 100f;
                vertices[i * 3 + 2] = triangleVertices[i].Z / 100f;
                //Debug.WriteLine(vertices[i]);
            }

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);

            // 赤色に設定
            GL.Color3(1.0f, 0.0f, 0.0f);  // 赤色

            // 頂点データを指定
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);

            // 三角形を描画
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, triangleVertices.Length);
        }

        private void DrawTriangles(Vector3[] triangleVertices, Vector3[] triangleNormals)
        {
            // 頂点バッファを生成し、データを転送
            if (vertexBuffer == 0)
            {
                GL.GenBuffers(1, out vertexBuffer);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(triangleVertices.Length * sizeof(float)), triangleVertices, BufferUsageHint.StaticDraw);
            }
            else
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(triangleVertices.Length * sizeof(float)), triangleVertices, BufferUsageHint.StaticDraw);
            }

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);

            // シェーディングを設定（ライト方向と法線計算）
            for (int i = 0; i < triangleVertices.Length; i += 3)
            {
                if (i == 3) i = 2;
                Vector3 A = triangleVertices[i] / 1f;
                Vector3 B = triangleVertices[i + 1] / 1f;
                Vector3 C = triangleVertices[i + 2] / 1f;

                Vector3 normal = triangleNormals[i / 3] / 1f;
                Vector3 lightDirection = (lightPosition - A).Normalized();

                // 法線とライト方向の内積をシェーディングに使う
                float intensity = Math.Max(Vector3.Dot(normal, lightDirection), 0.0f);

                GL.Color3(intensity, 0.0f, 0.0f);  // 赤色シェーディング

                //GL.DrawArrays(PrimitiveType.Triangles, i, 3);
            }

            Vector3[] vertices = new Vector3[triangleVertices.Length];
            for (int a = 0; a < triangleVertices.Length; a++)
            {
                vertices[a] = triangleVertices[a] / 1000f;
            }

            //int vertexBuffer;
            GL.GenBuffers(1, out vertexBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, vertices.Length);

            //for (int i = 0; i < triangleVertices.Length; i += 3)
            //{
            //    if (i == 3) i = 2;
            //    // 三角形の3頂点を取得
            //    Vector3 A = triangleVertices[i] / 10000f;
            //    Vector3 B = triangleVertices[i + 1] / 10000f;
            //    Vector3 C = triangleVertices[i + 2] / 10000f;

            //    // 法線ベクトルを計算
            //    Vector3 normal = triangleNormals[i/3] / 10000f;

            //    // ライトの方向ベクトルを計算
            //    Vector3 lightDirection = (lightPosition - A).Normalized();

            //    // 法線とライトの内積を計算してシェーディング
            //    float intensity = Math.Max(Vector3.Dot(normal, lightDirection), 0.0f);

            //    // 法線の方向に基づいて色を設定
            //    GL.Color3(intensity, 0.0f, 0.0f); // 赤色を基調にシェーディング

            //    // 三角形を描画
            //    float[] vertices = new float[]
            //    {
            //        A.X, A.Y, A.Z,
            //        B.X, B.Y, B.Z,
            //        C.X, C.Y, C.Z
            //    };

            //    int vertexBuffer;
            //    GL.GenBuffers(1, out vertexBuffer);
            //    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);

            //    GL.EnableClientState(ArrayCap.VertexArray);
            //    GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
            //    GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 3);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            glControl.Invalidate();
        }

        private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);  // カメラの初期位置
        private Vector2 cameraRotation = new Vector2(0.0f, 0.0f);  // カメラの初期回転
        private float cameraDistance = 3.0f;  // カメラの回転軸に対する位置
        private float cameraSpeed = 0.5f;  // カメラの移動速度
        private Vector3 lightPosition = new Vector3(0.0f, 2.0f, 2.0f);  // ライトの位置
        private int vertexBuffer;

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {   // 前に移動
                cameraPosition.X += cameraSpeed * (float)Math.Cos(cameraRotation.X);
                cameraPosition.Z -= cameraSpeed * (float)Math.Sin(cameraRotation.X);
            }
            if (e.KeyCode == Keys.S)
            {   // 後ろに移動
                cameraPosition.X -= cameraSpeed * (float)Math.Cos(cameraRotation.X);
                cameraPosition.Z += cameraSpeed * (float)Math.Sin(cameraRotation.X);
            }
            if (e.KeyCode == Keys.A)
            {   // 左に移動
                cameraPosition.Z -= cameraSpeed * (float)Math.Cos(cameraRotation.X);
                cameraPosition.X -= cameraSpeed * (float)Math.Sin(cameraRotation.X);
            }
            if (e.KeyCode == Keys.D)
            {   // 右に移動
                cameraPosition.Z += cameraSpeed * (float)Math.Cos(cameraRotation.X);
                cameraPosition.X += cameraSpeed * (float)Math.Sin(cameraRotation.X);
            }
            if (e.KeyCode == Keys.Q)
            {   // 上に移動
                cameraPosition.Y += cameraSpeed;
            }
            if (e.KeyCode == Keys.E)
            {   // 下に移動
                cameraPosition.Y -= cameraSpeed;
            }

            glControl.Invalidate();  // 描画を更新
        }
        private Point BeforeMousePoint = new Point();
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            float deltaX = e.X - BeforeMousePoint.X;
            float deltaY = e.Y - BeforeMousePoint.Y;
            BeforeMousePoint = new Point(e.X, e.Y);

            if (e.Button != MouseButtons.None)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:

                        deltaX *= 0.005f;
                        deltaY *= 0.005f;

                        cameraRotation.X += deltaX;
                        cameraRotation.Y += deltaY;
                        const double halfPi = Math.PI * 0.5;
                        if (Math.Abs(cameraRotation.Y) > halfPi)
                        {
                            cameraRotation.Y = (float)halfPi * ((cameraRotation.Y < 0) ? -0.999f : 0.999f);
                        }
                        break;
                    case MouseButtons.Right:
                        break;
                }
                glControl.Invalidate();  // 描画を更新
            }
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // カメラの投影行列設定 (透視投影)
            float aspectRatio = glControl.Width / glControl.Height;
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100f);
            GL.LoadMatrix(ref projectionMatrix);

            GL.MatrixMode(MatrixMode.Modelview); // モデルビュー行列に切り替え
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            //SetProjection();
            //glControl.SwapBuffers();
            //glControl.Refresh();
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            cameraDistance += -0.01f * e.Delta;

            glControl.Invalidate();  // 描画を更新
        }


        private void MainWindow_Load(object sender, EventArgs e)
        {

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
