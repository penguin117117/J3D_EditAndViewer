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
using System.Runtime.InteropServices;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;

namespace J3DEditorAndViewer.UI.Renderer.Library.GX
{
    internal class GXVertexDataOffset
    {
        public static nint PositionMatrix => Marshal.OffsetOf<GXElement>(nameof(GXElement.PositionMatrix));
        public static nint TexMat0t3 => Marshal.OffsetOf<GXElement>(nameof(GXElement.TexMatrix));
        public static nint TexMat4t7 => Marshal.OffsetOf<GXElement>(nameof(GXElement.TexMatrix));
        public static nint Pos_NBT_Color0t1 => Marshal.OffsetOf<GXElement>(nameof(GXElement.Position));
        public static nint TexCoord0t3 => Marshal.OffsetOf<GXElement>(nameof(GXElement.TexCoord));
        public static nint TexCoord4t7 => Marshal.OffsetOf<GXElement>(nameof(GXElement.TexCoord));
    }
}
