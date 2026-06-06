using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.Model.GX
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct VirtexBufferObject
    {
        Vector4 Position;
        Vector4 Normal;
        Vector4 Tangent;
        Vector4 Binormal;
        Vector4 Color0;
        Vector4 Color1;
        Vector2 TextureCoordinate0;
        Vector2 TextureCoordinate1;
        Vector2 TextureCoordinate2;
        Vector2 TextureCoordinate3;
        Vector2 TextureCoordinate4;
        Vector2 TextureCoordinate5;
        Vector2 TextureCoordinate6;
        Vector2 TextureCoordinate7;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct VirtexMatrixBufferObject
    {
        int TextureMatrix0;
        int TextureMatrix1;
        int TextureMatrix2;
        int TextureMatrix3;
        int TextureMatrix4;
        int TextureMatrix5;
        int TextureMatrix6;
        int TextureMatrix7;
        int PositionMatrix7;
    }
}
