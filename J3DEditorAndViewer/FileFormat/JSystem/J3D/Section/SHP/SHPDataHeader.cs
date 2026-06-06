using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static J3DEditorAndViewer.FileFormat.SectionFormat.SHP1;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP
{
    public class SHPDataHeader
    {
        public int ShapeDataOffset { get; private set; }

        public enum Key
        {
            RemapTable,
            NameTable,
            VertexIndexAttributes,
            Matrices,
            Primitives,
            MatrixData,
            PrimitiveTable,
        }

        public class Continer
        {
            public int Offset { get; private set; }
            public int LengthInByte { get; private set; }

            public Continer(int offset)
            {
                Offset = offset;
                LengthInByte = 0;
            }

            public void SetLengthInByteFromNextOffset(int offset)
            {
                LengthInByte = offset - Offset;
                if (LengthInByte < 0)
                {
                    LengthInByte = 0;
                    throw new AggregateException("指定された位置は自身の位置より前です。");
                }
            }
        }
        public Dictionary<Key, Continer> ArrayDataPair { get; private set; }


        public SHPDataHeader(EndianBinaryReaderBase br, int sHPSectionLength)
        {
            ArrayDataPair = new();

            ShapeDataOffset = br.ReadInt32();
            ArrayDataPair.Add(Key.RemapTable, new(br.ReadInt32()));
            ArrayDataPair.Add(Key.NameTable, new(br.ReadInt32()));
            ArrayDataPair.Add(Key.VertexIndexAttributes, new(br.ReadInt32()));
            ArrayDataPair.Add(Key.Matrices, new(br.ReadInt32()));
            ArrayDataPair.Add(Key.Primitives, new(br.ReadInt32()));
            ArrayDataPair.Add(Key.MatrixData, new(br.ReadInt32()));
            ArrayDataPair.Add(Key.PrimitiveTable, new(br.ReadInt32()));

            var SortByOffset = ArrayDataPair.OrderBy(x => x.Value.Offset).ToList();

            for (int i = 1; i < SortByOffset.Count(); i++)
            {
                SortByOffset[i - 1].Value.SetLengthInByteFromNextOffset(SortByOffset[i].Value.Offset);
            }
            SortByOffset.Last().Value.SetLengthInByteFromNextOffset(sHPSectionLength);
        }
    }
}