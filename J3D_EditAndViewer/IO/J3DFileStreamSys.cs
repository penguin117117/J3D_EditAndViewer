using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;

namespace J3D_EditAndViewer.IO
{
    public class J3DFileStreamSys
    {
        private const int ByteFormattingFactor = 32;
        public static void PaddingSkip(BinaryReader br) 
        {
            while (true) 
            {
                if (br.BaseStream.Position % ByteFormattingFactor == 0) break;
                br.ReadByte();
            }
        }

        public static Vector3 ReadFloatVector3(BinaryReader br)
        {
            return new Vector3(BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br));
        }

        public static Vector3h ReadShortVector3(BinaryReader br)
        {
            return new Vector3h(BigEndian.ReadInt16(br), BigEndian.ReadInt16(br), BigEndian.ReadInt16(br));
        }
    }
}
