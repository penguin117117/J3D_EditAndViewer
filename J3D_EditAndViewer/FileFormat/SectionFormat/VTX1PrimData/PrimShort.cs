using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.IO;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    public class PrimShort : IPrim
    {
        public Vector3 GetData { get; private set; }

        public Vector3 Set(BinaryReader br, byte shiftBit)
        {
            return new Vector3(BigEndian.ReadInt16(br) >> shiftBit, BigEndian.ReadInt16(br) >> shiftBit, BigEndian.ReadInt16(br) >> shiftBit);
        }
    }
}
