using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX.Color
{
    internal class RGB8 : IColor
    {
        public int SizeOfBytes => 3;
        public Vector4i Read(EndianBinaryReaderBase br)
        {
            int r = br.ReadByte();
            int g = br.ReadByte();
            int b = br.ReadByte();

            return new(r, g, b, 255);
        }
    }
}
