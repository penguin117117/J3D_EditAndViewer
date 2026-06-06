using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem
{
    internal class SectionHeader : ISectionHeader
    {
        public long BeginPosition { get; private set; }
        public uint Type { get; private set; }
        public int SizeInBytes { get; private set; }

        public SectionHeader(EndianBinaryReaderBase br)
        {
            BeginPosition = br.BaseStream.Position;
            Type = br.ReadUInt32();
            SizeInBytes = br.ReadInt32();
        }
    }
}
