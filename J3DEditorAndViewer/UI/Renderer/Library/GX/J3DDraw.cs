// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.FileFormat.JSystem.J3D;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.UBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Command;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Array.VAO;

namespace J3DEditorAndViewer.UI.Renderer.Library.GX
{
    // GXDraw X1 = ShapeDraw X1
    internal class J3DDraw
    {
        // (カメラ)投影行列
        public struct ProjectionMatrix
        {
            public Matrix4 View { get; set; } = Matrix4.Identity;
            public Matrix4 Projection { get; set; } = Matrix4.Identity;

            public ProjectionMatrix() { }
        }

        public GXVertexBufferObject GXVertexBuffer { get; private set; }
        public GXElementBufferObject[] GXElementBuffer { get; private set; }

        public IShader Shader { get; }
        public Dictionary<string, IVAOCommonManager> VAOs { get; }

        public J3DDraw(VertexObjectArrays gXVertexObject, ShapeData shapeData)
        {
            GXVertexBuffer = new(gXVertexObject);
            GXElementBuffer = new GXElementBufferObject[shapeData.Primitives.Length];

            for (var i = 0; i < GXElementBuffer.Length; i++)
            {
                GXElementBuffer[i] = new(shapeData.Primitives[i], GXVertexBuffer.Offset);
            }

        }

        public void Use()
        {

        }
    }
}
