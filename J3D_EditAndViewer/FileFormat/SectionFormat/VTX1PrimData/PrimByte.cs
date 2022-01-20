using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;

namespace J3D_EditAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    public class PrimByte : IPrim
    {
        public Vector3 GetData { get; private set; }

        public Vector3 Set(BinaryReader br)
        {
            return new Vector3(br.ReadByte(), br.ReadByte(), br.ReadByte());
        }


    }
}
