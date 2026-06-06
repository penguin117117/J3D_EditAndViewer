using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public class RGBA8 : IVertexColors
    {
        public Vector4 GetColorVector4 { get; private set; }

        public Vector4 GetColor(BinaryReader br)
        {
            // 4bytes
            GetColorVector4 = new Vector4(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
            return GetColorVector4;
        }
    }
}
