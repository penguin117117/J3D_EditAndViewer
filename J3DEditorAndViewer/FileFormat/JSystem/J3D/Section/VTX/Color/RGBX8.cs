// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX.Color
{
    internal class RGBX8 : IColor
    {
        public int SizeOfBytes => 3;
        public Vector4i Read(EndianBinaryReaderBase br)
        {
            int r = br.ReadByte();
            int g = br.ReadByte();
            int b = br.ReadByte();

            return new(r, g, b, 0xFF);
        }
    }
}
