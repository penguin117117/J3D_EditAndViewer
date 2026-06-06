// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.INF;
namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX
{
    public class VTX
    {
        private ISectionHeader Header;
        public VTXDataHeader SectionHeader { get; private set; }

        private IINFVertex INFVertex;

        public int VertexCount => INFVertex.VertexCount;
        public VertexObjectArrays VertexObjectArrays { get; private set; }

        public readonly uint[] J3DSectionType = [
           BitCast.ASCIIToInt($"{nameof(VTX)}1"),
        ];
        public VTX() { }
        public void Read(EndianBinaryReaderBase br, IINFVertex iNFVertex)
        {
            Read(new SectionHeader(br), br, iNFVertex);
        }
        public void Read(ISectionHeader header, EndianBinaryReaderBase br, IINFVertex iNFVertex)
        {
            Header = header;
            INFVertex = iNFVertex;

            if (!J3DSectionType.Contains(Header.Type))
            {
                throw new ArgumentException($"Type: {BitCast.IntToASCII(Header.Type)} がINFXではありません。");
            }

            SectionHeader = new(br, header.SizeInBytes);

            VertexObjectArrays = new(br, header, SectionHeader, iNFVertex);

            if (br.BaseStream.Position > (Header.BeginPosition + Header.SizeInBytes))
            {
                throw new Exception("JSystem.Sectionの整合性が取れません。");
            }

            br.BaseStream.Seek(header.BeginPosition + header.SizeInBytes, SeekOrigin.Begin);
            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
        }
    }
}
