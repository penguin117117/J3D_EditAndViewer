// OpenTK
using J3DEditorAndViewer.FileFormat.SectionFormat;
using J3DEditorAndViewer.IO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Array.VAO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.EBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.UBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace J3DEditorAndViewer.UI.Renderer
{

    internal class OpenGL4 : IRenderer
    {
        bool _disposed = false;

        //
        // RendererInterface
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        // 以下のコードはテストの為の実装です。
        // 本来であれば別途のファイルを読み込ませるようにするなど、改修が必要です。
        private static string VShaderCode = @"
out vec4 VertexColor;

void main() {
  gl_Position = Projection * View * Model * vec4(Position, 1.0);

  //VertexColor = Color0;
  VertexColor = mix(Color0, Color1, Mixer);
}
";
        private static string FShaderCode = @"
in vec4 VertexColor;

out vec4 FragColor;

void main() {
  // FragColor = VertexColor;
  FragColor = vec4(1f);
}
";

        public OpenGL4(J3DFileDialog j3d_FileDialog, int width, int height)
        {
            Width = width;
            Height = height;

            glslTypeTrait = new GLSLV430Traits();

            VBOManager = new StructVBOManager<VTX1Data>(j3d_FileDialog.J3DData.Model.VerTexData.GetData());
            VAOCommonManager = new VAOManager([VBOManager]);

            UniformManagerProjection = new StructUBOManager<ProjectionUniform>("CoordinateUniform", new ProjectionUniform());
            UniformManagerMixer = new StructUBOManager<MixerUniform>("MixerUniform", new MixerUniform());

            VertexFactory = new VertexFactory(VAOCommonManager, [UniformManagerProjection, UniformManagerMixer], glslTypeTrait, VShaderCode);
            FragmentFactory = new FragmentFactory(glslTypeTrait, FShaderCode);

            VEOManager = new TriangleEBOManager(j3d_FileDialog.J3DData.Model.ShapeData.GetTriangleindexes().ToArray());

            Shader = new TestShader(VertexFactory, FragmentFactory);

            // カメラ
            CameraPosition = new(0.0f, 0.0f, 0.0f);
            CameraAngle = new(0.0f, 0.0f);
            CameraDistance = 3.0f;
        }
        ~OpenGL4()
        {
            Dispose();
        }

        public void Reset()
        {
            return;
        }
        public void Resize(int _Width, int _Height)
        {
            Width = _Width;
            Height = _Height;
            GL.Viewport(0, 0, Width, Height);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Shader.Dispose();
            UniformManagerProjection.Dispose();
            UniformManagerMixer.Dispose();
            VAOCommonManager.Dispose();
            VBOManager.Dispose();
            VEOManager.Dispose();
        }

        // ストラクトレイアウト: https://ufcpp.net/study/csharp/interop/memorylayout/#layout-kind
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        // [StructLayout(LayoutKind.Explicit)]
        struct ProjectionUniform
        {
            public Matrix4 Model;
            public Matrix4 View;
            public Matrix4 Projection;
        }

        struct MixerUniform
        {
            public float Mixer;
        }

        // これらオブジェクト系列はdispose挙動の見直しが必要。
        private IGLSLTypeTraits glslTypeTrait;
        private IVBOManager<VTX1Data> VBOManager;
        private IVAOCommonManager VAOCommonManager;
        private IUBOManager<ProjectionUniform> UniformManagerProjection;
        private IUBOManager<MixerUniform> UniformManagerMixer;
        private IEBOManager VEOManager;
        private IVertexFactory VertexFactory;
        private IFragmentFactory FragmentFactory;
        private IShader Shader;



        // Camera
        private Vector3 cameraPosition;
        private float cameraDistance;
        private Vector2 cameraAngle;
        public ref Vector3 CameraPosition => ref cameraPosition;
        public ref float CameraDistance => ref cameraDistance;
        public ref Vector2 CameraAngle => ref cameraAngle;

        private void UpdateModelPosition()
        {
            UniformManagerProjection.Data.Model = Matrix4.CreateScale(0.01f);
        }

        private Quaternion rotate;

        private void UpdateViewPosition()
        {
            //Vector3 cameraPos = new(cameraPosition);
            //cameraPos.X -= cameraDistance * ((float)Math.Sin(cameraAngle.Y) * (float)Math.Cos(cameraAngle.X));
            //cameraPos.Y -= cameraDistance * ((float)Math.Sin(cameraAngle.X));
            //cameraPos.Z -= cameraDistance * ((float)Math.Cos(cameraAngle.Y) * (float)Math.Cos(cameraAngle.X));

            Vector3 cameraPos = new Vector3(0.0f, 0.0f, cameraDistance);
            cameraPos = Quaternion.FromAxisAngle(Vector3.UnitX, cameraAngle.X) * cameraPos;
            cameraPos = Quaternion.FromAxisAngle(Vector3.UnitY, cameraAngle.Y) * cameraPos;

            // カメラの位置を計算して、視点行列を適用。
            UniformManagerProjection.Data.View = Matrix4.LookAt(cameraPos + cameraPosition, cameraPosition, Vector3.UnitY);
        }

        private void UpdateProjection(float fovY = 45.0f, float depthNear = 0.01f, float depthFar = 1000.0f)
        {
            UniformManagerProjection.Data.Projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fovY),
                (float)Width / (float)Height,
                depthNear,
                depthFar);
        }

        public void Update(GLControl surface)
        {
            ErrorCode error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                Debug.WriteLine($"OpenGL Error: {error}");
                return;
            }

            GL.Clear(ClearBufferMask.ColorBufferBit);
            surface.Invalidate();

            ///
            /// 

            // プログラムの指定。
            Shader.Use();


            // Uniformデータの更新とセットアップ。
            UpdateModelPosition();
            UpdateViewPosition();
            UpdateProjection();
            UniformManagerProjection.Use();

            UniformManagerMixer.Data.Mixer = 0.0f;
            UniformManagerMixer.Use();

            // 頂点データの指定。
            VAOCommonManager.Use(VEOManager);

            ///
            ///

            // バッファの交換
            surface.SwapBuffers();
        }
    }
}

