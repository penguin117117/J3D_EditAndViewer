using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;

namespace J3D_EditAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public interface IVertexColors
    {
        Vector4 GetColorVector4 { get; }
        void GetColor(BinaryReader br);
    }
}
