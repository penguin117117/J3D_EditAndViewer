using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types.GXAttribPointerIO
{
    internal class GXShort : IGXAttribPointerIO
    {
        public int SizeInBytes => sizeof(short);

        public GXShort() { }

        public short ReadRaw(EndianBinaryReaderBase br)
        {
            return br.ReadInt16();
        }
        public float ReadSingle(EndianBinaryReaderBase br)
        {
            return ReadRaw(br);
        }
        public int ReadInt32(EndianBinaryReaderBase br)
        {
            return ReadRaw(br);
        }
        public float ReadFromFixedSingle(EndianBinaryReaderBase br, int rotr)
        {
            // rotrの名称やここでの左シフトはどちらも正しいです。
            return ReadRaw(br) / (0x01 << rotr);
        }
    }
}
