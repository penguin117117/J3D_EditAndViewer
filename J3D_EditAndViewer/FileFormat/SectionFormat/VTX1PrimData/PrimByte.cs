using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    public class PrimByte : IPrim
    {
        public Vector3 GetData { get; private set; }

        public Vector3 Set(BinaryReader br, byte shiftBit)
        {
            return new Vector3(br.ReadByte() >> shiftBit, br.ReadByte() >> shiftBit, br.ReadByte() >> shiftBit);
        }


    }
}
