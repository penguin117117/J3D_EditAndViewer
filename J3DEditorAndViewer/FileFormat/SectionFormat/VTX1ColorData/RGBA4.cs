// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public class RGBA4 : IVertexColors
    {
        public Vector4 GetColorVector4 { get; private set; }

        public Vector4 GetColor(BinaryReader br)
        {
            // 2bytes
            throw new NotImplementedException();
        }
    }
}
