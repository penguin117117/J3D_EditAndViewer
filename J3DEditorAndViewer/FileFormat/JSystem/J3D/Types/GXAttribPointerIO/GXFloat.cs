using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types.GXAttribPointerIO
{
    internal class GXFloat : IGXAttribPointerIO
    {
        public int SizeInBytes => sizeof(float);

        public GXFloat() { }

        public float ReadRaw(EndianBinaryReaderBase br)
        {
            return br.ReadSingle();
        }
        public float ReadSingle(EndianBinaryReaderBase br)
        {
            return ReadRaw(br);
        }
        public int ReadInt32(EndianBinaryReaderBase br)
        {
            return (int)ReadRaw(br);
        }
        public float ReadFromFixedSingle(EndianBinaryReaderBase br, int rotr)
        {
            return ReadRaw(br);
        }
    }
}
