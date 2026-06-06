using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP
{
    public class SHP
    {
        private ISectionHeader Header;
        private SHPDataHeader DataHeader;

        // private RemapTable; // 実際のデータが分からないためコメントのみ。

        public readonly uint[] J3DSectionType = [
           BitCast.ASCIIToInt($"{nameof(SHP)}1"),
        ];

        public ShapeData[] ShapeDatas;

        public SHP() { }

        public void Read(EndianBinaryReaderBase br)
        {
            Read(new SectionHeader(br), br);
        }
        public void Read(ISectionHeader header, EndianBinaryReaderBase br)
        {
            Header = header;
            int ShapeLength = br.ReadInt16();
            br.ReadInt16();

            DataHeader = new SHPDataHeader(br, header.SizeInBytes);

            ShapeDatas = ShapeData.Read(br, header.BeginPosition, ShapeLength, DataHeader);

            if (br.BaseStream.Position > Header.BeginPosition + Header.SizeInBytes)
            {
                throw new Exception("JSystem.Sectionの整合性が取れません。");
            }

            br.BaseStream.Seek(header.BeginPosition + header.SizeInBytes, SeekOrigin.Begin);
            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
        }
    }
}
