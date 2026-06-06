using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer
{
    internal interface IBuffer : IFactory
    {
        int WriteDefinicator(StringBuilder builder, Type.IGLSLTypeTraits typeTrait, int beginIndex = 0);
    }
}
