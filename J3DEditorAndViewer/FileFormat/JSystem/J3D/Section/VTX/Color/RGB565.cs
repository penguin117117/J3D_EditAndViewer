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
    internal class RGB565 : IColor
    {
        public int SizeOfBytes => 2;

        public Vector4i Read(EndianBinaryReaderBase br)
        {
            Vector4i bufValue = new();
            int rgb = br.ReadUInt16();
            bufValue.X = (rgb & 0b1111100000000000) >> 11;
            bufValue.Y = (rgb & 0b0000011111100000) >> 5;
            bufValue.Z = (rgb & 0b0000000000011111);
            bufValue.W = 255;

            return IColor.Regix(bufValue, 5, 6, 5, 8);
        }
    }
}
