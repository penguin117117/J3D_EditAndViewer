using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.Model.GX
{
    // gpu index decode mode でも使用されます。
    [StructLayout(LayoutKind.Sequential)]
    public struct Element
    {
        // group 0(int)
        public int PositionMatrix = -1;
        // group 1(ivec4)
        public int TexMatrix0 = -1;
        public int TexMatrix1 = -1;
        public int TexMatrix2 = -1;
        public int TexMatrix3 = -1;
        // group 2(ivec4)
        public int TexMatrix4 = -1;
        public int TexMatrix5 = -1;
        public int TexMatrix6 = -1;
        public int TexMatrix7 = -1;
        // group3(ivec4)
        public int Position = -1;
        public int NBT = -1;
        public int Color0 = -1;
        public int Color1 = -1;
        // group 4(ivec4)
        public int TexCoord0 = -1;
        public int TexCoord1 = -1;
        public int TexCoord2 = -1;
        public int TexCoord3 = -1;
        public int TexCoord4 = -1;
        public int TexCoord5 = -1;
        public int TexCoord6 = -1;
        public int TexCoord7 = -1;


        public Span<int> TexMatrix => MemoryMarshal.CreateSpan(ref TexMatrix0, 8);
        public Span<int> Color => MemoryMarshal.CreateSpan(ref Color0, 8);
        public Span<int> TexCoord => MemoryMarshal.CreateSpan(ref Color0, 8);
        public Element() { }
    }
}
