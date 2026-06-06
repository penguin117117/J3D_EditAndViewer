using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.Model.GX
{
    public enum PrimitiveType
    {
        Quad = 0x80,
        Triangle = 0x90,
        TriangleStrip = 0x98,
        TriangleFan = 0xa0,
        Line = 0xB0,
        Point = 0xB8,
    }
}
