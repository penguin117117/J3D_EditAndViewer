// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    public class PrimByte : IPrim
    {
        public Vector3 GetData { get; private set; }

        public Vector3 Set(BinaryReader br, byte shiftBit)
        {
            float divValue = (float)(0x01 << shiftBit);
            return new Vector3(br.ReadByte() / divValue, br.ReadByte() / divValue, br.ReadByte() / divValue);
        }


    }
}
