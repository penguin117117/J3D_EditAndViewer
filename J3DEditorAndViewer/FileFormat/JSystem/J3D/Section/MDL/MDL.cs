using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.MDL
{
    public class MDL
    {
        private ISectionHeader Header;
        public MDL() { }

        public readonly uint[] J3DSectionType = [
            BitCast.ASCIIToInt($"{nameof(MDL)}3"),
        ];
        public void Read(EndianBinaryReaderBase br)
        {
            Read(new SectionHeader(br), br);
        }
        public void Read(ISectionHeader header, EndianBinaryReaderBase br)
        {
            Header = header;

            // TODO:

            if (br.BaseStream.Position > Header.BeginPosition + Header.SizeInBytes)
            {
                throw new Exception("JSystem.Sectionの整合性が取れません。");
            }

            br.BaseStream.Seek(header.BeginPosition + header.SizeInBytes, SeekOrigin.Begin);
            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
        }
    }
}
