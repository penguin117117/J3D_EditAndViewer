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
using System.IO;
using OpenTK;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public interface IVertexColors
    {
        Vector4 GetColorVector4 { get; }
        Vector4 GetColor(BinaryReader br);
    }
}
