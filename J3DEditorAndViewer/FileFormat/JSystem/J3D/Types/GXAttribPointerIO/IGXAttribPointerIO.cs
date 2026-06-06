using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types.GXAttribPointerIO
{
    public interface IGXAttribPointerIO
    {
        public int SizeInBytes { get; }

        public float ReadSingle(EndianBinaryReaderBase br);
        public int ReadInt32(EndianBinaryReaderBase br);
        public float ReadFromFixedSingle(EndianBinaryReaderBase br, int rotr);

        public static IGXAttribPointerIO GetIO(GXAttribPointerType type)
        {
            return type switch
            {
                GXAttribPointerType.Byte => new GXByte(),
                GXAttribPointerType.SByte => new GXSByte(),
                GXAttribPointerType.Short => new GXShort(),
                GXAttribPointerType.UnsignedShort => new GXUShort(),
                GXAttribPointerType.Float => new GXFloat(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
