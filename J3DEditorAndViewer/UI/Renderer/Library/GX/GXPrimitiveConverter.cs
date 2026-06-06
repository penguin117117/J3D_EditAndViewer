// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using DocumentFormat.OpenXml.Vml;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Library.GX
{
    public static class GXPrimitiveConverter
    {
        public static PrimitiveType ToGLPrimitive(GXPrimitiveType gXPrimitive)
        {
            return gXPrimitive switch
            {
                GXPrimitiveType.Triangle => PrimitiveType.Triangles,
                GXPrimitiveType.TriangleStrip => PrimitiveType.TriangleStrip,
                GXPrimitiveType.TriangleFan => PrimitiveType.TriangleFan,
                GXPrimitiveType.Line => PrimitiveType.Lines,
                GXPrimitiveType.Point => PrimitiveType.Points,

                GXPrimitiveType.Quad => throw new NotImplementedException(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
