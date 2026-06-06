using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using J3DEditorAndViewer.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem
{
    internal class Header : IHeader
    {
        public long BeginPosition { get; private set; }
        public uint SubSystem { get; private set; }
        public uint SubSystemType { get; private set; }
        public int SizeInBytes { get; private set; }
        public int SectionCount { get; private set; }
        public int SubSystemMarker { get; private set; }

        public Header(EndianBinaryReaderBase br)
        {
            BeginPosition = br.BaseStream.Position;
            SubSystem = br.ReadUInt32();
            SubSystemType = br.ReadUInt32();
            SizeInBytes = br.ReadInt32();
            SectionCount = br.ReadInt32();
            SubSystemMarker = br.ReadInt32();
            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
        }
    }
}
