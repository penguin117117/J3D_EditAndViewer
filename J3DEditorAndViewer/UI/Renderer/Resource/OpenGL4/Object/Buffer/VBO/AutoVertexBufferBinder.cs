// OpenTK
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO
{
    internal sealed class AutoVertexBufferBinder : AutoBufferBinder
    {
        public AutoVertexBufferBinder(int handle) : base(BufferTarget.ArrayBuffer, handle) { }

        public new IAutoObjectBinder MoveObject()
        {
            if (disposed == true) throw new ObjectDisposedException(GetType().Name);
            disposed = true;
            return new AutoVertexBufferBinder(Handle);
        }
    }
}
