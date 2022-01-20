using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3D_EditAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public class RGBA4 : IVertexColors
    {
        public Vector4 GetColorVector4 => throw new NotImplementedException();

        public void GetColor(BinaryReader br)
        {
            throw new NotImplementedException();
        }
    }
}
