// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.EBO
{
    internal class AutoElementArrayBufferBinder : AutoBufferBinder
    {
        public AutoElementArrayBufferBinder(int handle) : base(BufferTarget.ElementArrayBuffer, handle) { }

        public new IAutoObjectBinder MoveObject()
        {
            if (disposed == true) throw new ObjectDisposedException(GetType().Name);
            disposed = true;
            return new AutoElementArrayBufferBinder(Handle);
        }
    }
}
