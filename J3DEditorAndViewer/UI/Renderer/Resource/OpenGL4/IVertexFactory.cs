// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4
{
    internal interface IVertexFactory : IFactory
    {
        ShaderType ShaderType { get; }
        int WriteDefinicator(StringBuilder builder);
    }
}
