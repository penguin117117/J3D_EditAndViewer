using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.EBO
{
    internal interface IEBOManager : IEBOCommonManager
    {
        public uint[] Data { get; set; }
    }
}
