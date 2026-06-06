using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public class RGB8 : IVertexColors
    {
        public Vector4 GetColorVector4 => throw new NotImplementedException();

        public Vector4 GetColor(BinaryReader br)
        {
            // 3bytes
            throw new NotImplementedException();
        }
    }
}
