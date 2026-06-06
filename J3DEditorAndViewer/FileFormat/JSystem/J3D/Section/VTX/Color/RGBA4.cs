// OpenTK
//
using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX.Color
{
    internal class RGBA4 : IColor
    {
        public int SizeOfBytes => 2;
        public Vector4i Read(EndianBinaryReaderBase br)
        {
            int rg = br.ReadByte();
            int ba = br.ReadByte();

            Vector4i bufValue = new();
            bufValue.X = (rg & 0b11110000) >> 4;
            bufValue.Y = (rg & 0b00001111);
            bufValue.Z = (ba & 0b11110000) >> 4;
            bufValue.W = (ba & 0b00001111);

            return IColor.Regix(bufValue, 4, 4, 4, 4);
        }
    }
}
