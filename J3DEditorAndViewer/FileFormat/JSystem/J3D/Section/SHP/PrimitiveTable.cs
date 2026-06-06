using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP
{
    public class PrimitiveTable
    {
        public int LengthInByte { get; private init; }
        public int Offset { get; private init; }

        public PrimitiveTable(EndianBinaryReaderBase br)
        {
            // "4 bytes long"とのことなので、shortかもしれません。
            LengthInByte = br.ReadInt32();
            Offset = br.ReadInt32();
        }

        public static PrimitiveTable[] Read(EndianBinaryReaderBase br, in long beginPos, SHPDataHeader sHPDataHeader)
        {
            var header = sHPDataHeader.ArrayDataPair[SHPDataHeader.Key.PrimitiveTable];

            PrimitiveTable[] retValue = new PrimitiveTable[header.LengthInByte / 8];// LengthInByte / テーブルの長さ(byte)
            for (int i = 0; i < retValue.Length; i++)
            {
                retValue[i] = new PrimitiveTable(br);
            }

            Debug.Assert(br.BaseStream.Position < (beginPos + header.Offset));

            return retValue;
        }
    }
}
