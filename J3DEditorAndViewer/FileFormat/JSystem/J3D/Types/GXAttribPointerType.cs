using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types
{
    public enum GXAttribPointerType : int
    {
        RGB565 = 0x00,
        RGB8 = 0x01,
        RGBX8 = 0x02,
        RGBA4 = 0x03,
        RGBA6 = 0x04,
        RGBA8 = 0x05,

        SByte = 0x00,
        Byte = 0x01,
        Short = 0x03,
        UnsignedShort = 0x02,
        Float = 0x04,
    }
}
