using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.TEX
{
    public class TEX
    {
        private ISectionHeader Header;
        public TEX() { }

        public readonly uint[] J3DSectionType = [
           BitCast.ASCIIToInt($"{nameof(TEX)}1"),
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
