// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type
{
    internal interface IGLSLType
    {
        public string Name { get; }
        public VertexAttribPointerType Type { get; }
        public int LengthOfType { get; }
    }
}
