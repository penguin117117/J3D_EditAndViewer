using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX
{
    public class VTXDataHeader
    {
        public int VertexAttributeOffset { get; private set; }

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

        public Dictionary<GXAttribType, Continer> ArrayDataPair { get; private set; } = new();

        public VTXDataHeader(EndianBinaryReaderBase br, in int vTXSectionLength)
        {
            VertexAttributeOffset = br.ReadInt32();

            ArrayDataPair.Add(GXAttribType.Position, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.NBT, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.NBT0, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.Color0, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.Color1, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord0, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord1, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord2, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord3, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord4, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord5, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord6, new(br.ReadInt32()));
            ArrayDataPair.Add(GXAttribType.TexCoord7, new(br.ReadInt32()));

            List<KeyValuePair<GXAttribType, Continer>> SortByOffset = ArrayDataPair.OrderBy(x => x.Value.Offset).ToList();

            for (int i = 1; i < SortByOffset.Count(); i++)
            {
                SortByOffset[i - 1].Value.SetLengthInByteFromNextOffset(SortByOffset[i].Value.Offset);
            }
            SortByOffset.Last().Value.SetLengthInByteFromNextOffset(vTXSectionLength);
        }
    }
}
