// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.EBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Array.VAO
{
    internal class VAOManager : IVAOCommonManager
    {
        private bool disposed = false;

        readonly private int Handle;

        public IReadOnlyList<IVBOCommonManager> VBOCommonManagers { get; private set; }

        private IAutoObjectBinder CreateObjectBinder()
        {
            return new AutoVartexArrayBinder(Handle);
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int location = 0)
        {
            location = 0;

            using var aob = CreateObjectBinder();
            foreach (var vbo in VBOCommonManagers)
            {
                vbo.WriteDefinicator(builder, typeTrait, location);
                location++;
            }

            return location;
        }

        public VAOManager(IReadOnlyList<IVBOCommonManager> vBOCommonManagers)
        {
            VBOCommonManagers = vBOCommonManagers;
            Handle = GL.GenVertexArray();
        }

        ~VAOManager()
        {
            if (!disposed)
            {
                throw new Exception("Dispose が呼ばれていません。");
            }
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            GL.DeleteVertexArray(Handle);
        }

        public void UpdateBuffer() => throw new NotImplementedException();

        public void Use(IEBOCommonManager eBOCommonManager)
        {
            foreach (var vbo in VBOCommonManagers)
            {
                vbo.Use();
            }
            using var aob = CreateObjectBinder();
            eBOCommonManager.Use();
        }

        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}
