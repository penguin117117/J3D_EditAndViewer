using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types
{
    // MEMO: LumasではNormalかつGXCompCnt(zgcn)の値によって操作されるらしい。
    // またzgcnでのNBTは確認されていないらしい。
    // Colorではアルファ在り無しの操作をGXCompCntでするため、
    // NBTがライト操作の値と括るならlumasの方が正解である可能性が高い。

    public enum GXAttribType : int
    {
        Null = 0xff,

        PositionMatrix = 0x0,

        TexMatrix0 = 0x1,
        TexMatrix1,
        TexMatrix2,
        TexMatrix3,
        TexMatrix4,
        TexMatrix5,
        TexMatrix6,
        TexMatrix7,

        Position = 0x9,

        Normal = 0xA,

        NBT = 0xA, // lumasより推測。

        Color0 = 0xb,
        Color1,

        TexCoord0 = 0xd,
        TexCoord1,
        TexCoord2,
        TexCoord3,
        TexCoord4,
        TexCoord5,
        TexCoord6,
        TexCoord7,

        // http://www.amnoid.de/gc/bmd.txt
        PositionMatirxArray = 0x15,
        NormalMatrixArray = 0x16,
        TexMatrixArray = 0x17,
        LightMatrixArray = 0x18,

        NBT0 = 0x19, // zgcn

        // http://www.amnoid.de/gc/bmd.txt
        MaxAttribute = 0x1a,
    }
}
