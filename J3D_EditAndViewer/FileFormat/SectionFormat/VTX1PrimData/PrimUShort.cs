﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J3D_EditAndViewer.IO;

namespace J3D_EditAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    public class PrimUShort : IPrim
    {
        public Vector3 GetData { get; private set; }

        public Vector3 Set(BinaryReader br)
        {
            return new Vector3(BigEndian.ReadUInt16(br), BigEndian.ReadUInt16(br), BigEndian.ReadUInt16(br));
        }
    }
}
