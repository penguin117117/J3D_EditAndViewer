using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO
{
    internal interface IVBOManager<InT> : IVBOCommonManager
        where InT : struct
    {
        public InT[] Data { get; set; }
    }
}
