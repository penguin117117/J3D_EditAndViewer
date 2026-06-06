using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types.GXAttribPointerIO;
using System.Collections.Generic;
using System.Diagnostics;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX
{
    internal class VertexAttribute
    {
        public GXAttribType Type { get; init; }
        public int LengthOfGXType { get; init; }
        public GXAttribPointerType GXAttribPointerType { get; init; }
        public byte GXCompShiftRight { get; init; }

        private IGXAttribPointerIO? Reader;

        public int SizeInBytes { get => Reader?.SizeInBytes * LengthOfGXType ?? throw new NullReferenceException(); set => throw new NotImplementedException(); }
        public VertexAttribute(GXAttribType type, int lengthOfGXType, GXAttribPointerType gXAttribPointerType, byte gXCompShiftRight)
        {
            Type = type;
            LengthOfGXType = lengthOfGXType;
            GXAttribPointerType = gXAttribPointerType;
            GXCompShiftRight = gXCompShiftRight;
            if (type is GXAttribType.Color0 or GXAttribType.Color1) { return; }
            Reader = IGXAttribPointerIO.GetIO(GXAttribPointerType);
        }

        public static List<VertexAttribute> ReadVertexAttributes(EndianBinaryReaderBase br, in long beginPos, in VTXDataHeader vTXSectionHeader)
        {
            Debug.WriteLine($"ReadVertexAttributes.BeginPos {beginPos + vTXSectionHeader.VertexAttributeOffset}");
            br.BaseStream.Seek(beginPos + vTXSectionHeader.VertexAttributeOffset, SeekOrigin.Begin);
            List<VertexAttribute> attributes = new();

            while (true)
            {
                var Type = (GXAttribType)br.ReadUInt32();
                if (Type is GXAttribType.Null)
                {
                    break;
                }

                var LengthOfGXType = GXAttrib.GetLengthOfGXCompType(br, Type);
                var GXAttribPointerType = (GXAttribPointerType)br.ReadInt32();
                var GXCompShiftRight = br.ReadByte();
                JSystemReadUtil.PaddingSkip.Alignment16Byte(br);
                attributes.Add(new(Type, LengthOfGXType, GXAttribPointerType, GXCompShiftRight));
            }

            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
            return attributes;
        }

        public float[][] ReadGXCompornent(EndianBinaryReaderBase br, int sizeInBytesOfArray)
        {
            if (Reader is null) { throw new NullReferenceException(); }
            float[][] retValue = new float[sizeInBytesOfArray / SizeInBytes][];

            for (int i = 0; i < retValue.Length; i++)
            {
                retValue[i] = new float[LengthOfGXType];

                for (int j = 0; j < LengthOfGXType; j++)
                {
                    retValue[i][j] = Reader.ReadFromFixedSingle(br, GXCompShiftRight);
                }
            }

            return retValue;
        }
    }
}
