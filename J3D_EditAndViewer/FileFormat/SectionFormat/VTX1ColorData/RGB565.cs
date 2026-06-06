using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1ColorData
{
    public class RGB565 : IVertexColors
    {
        public Vector4 GetColorVector4 { get; private set; }

        public Vector4 GetColor(BinaryReader br)
        {
            const int bit5 = 0b11111;
            const int bit6 = 0b111111;

            int buffer = br.ReadUInt16();

            float red = ((buffer >> (5 + 6)) & bit5) / bit5;
            float green = ((buffer >> 6) & bit6) / bit6;
            float blue = (buffer & bit5) / bit5;

            GetColorVector4 = new Vector4(red, green, blue, 1.0f);
            return GetColorVector4;
        }
    }
}
