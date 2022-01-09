using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
