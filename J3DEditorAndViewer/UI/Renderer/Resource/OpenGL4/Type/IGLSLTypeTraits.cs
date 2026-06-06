using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type
{
    internal interface IGLSLTypeTraits
    {
        public IGLSLType TypeOf(System.Type type);
    }
}
