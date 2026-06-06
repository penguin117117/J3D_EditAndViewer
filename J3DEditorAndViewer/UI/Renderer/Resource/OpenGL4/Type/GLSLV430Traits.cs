using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type
{
    internal class GLSLV430Traits : IGLSLTypeTraits
    {
        public GLSLV430Traits() { }

        public IGLSLType TypeOf(System.Type type)
        {
            return new GLSLV430(type);
        }
    }
}
