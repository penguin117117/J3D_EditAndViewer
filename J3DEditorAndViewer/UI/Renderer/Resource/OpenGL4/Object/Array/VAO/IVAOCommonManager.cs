using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.EBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Array.VAO
{
    internal interface IVAOCommonManager : IBuffer
    {
        public void Use(IEBOCommonManager eBOCommonManager);
    }
}
