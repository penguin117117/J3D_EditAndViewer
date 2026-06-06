using J3DEditorAndViewer.Model.GX;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer
{
    internal partial class GXOnOpenGL4 : IRenderer
    {

        public int Width => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        public ref Vector3 CameraPosition => throw new NotImplementedException();

        public ref float CameraDistance => throw new NotImplementedException();

        public ref Vector2 CameraAngle => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Resize(int Width, int Height)
        {
            throw new NotImplementedException();
        }

        public void Update(GLControl Surface)
        {
            throw new NotImplementedException();
        }
    }
}
