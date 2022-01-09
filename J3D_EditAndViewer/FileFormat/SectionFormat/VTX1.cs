using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.IO;
using J3D_EditAndViewer.FileFormat.SectionFormat.VTX1ColorData;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class VTX1
    {
        private long SectionBaseAddres;

        private string SectionName;
        private int SectionSize;
        private int VertexFormatOffset;
        private int PositionDataArrayOffset;
        private int NormalDataArrayOffset;
        private int NBT_DataArrayOffset;
        private int[] ColorDataArrayOffset;
        private int[] TexcoordDataArrayOffset;

        private byte VertexAttributeCount;

        public VTX1() 
        {
            ColorDataArrayOffset = new int[COLOR_ARRAY_MAX];
            TexcoordDataArrayOffset = new int[TEXTURE_ARRAY_MAX];
        }
        public void Read(BinaryReader br,int vertexCount) 
        {
            VertexAttributeCount = 0;
            SectionBaseAddres = br.BaseStream.Position;

            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);
            VertexFormatOffset = BigEndian.ReadInt32(br);
            PositionDataArrayOffset = BigEndian.ReadInt32(br);
            NormalDataArrayOffset = BigEndian.ReadInt32(br);
            NBT_DataArrayOffset = BigEndian.ReadInt32(br);

            DataCountAdder(VertexFormatOffset);
            DataCountAdder(PositionDataArrayOffset);
            DataCountAdder(NormalDataArrayOffset);
            DataCountAdder(NBT_DataArrayOffset);

            for (int i = 0; i < COLOR_ARRAY_MAX; i++) 
            {
                ColorDataArrayOffset[i] = BigEndian.ReadInt32(br);
                DataCountAdder(ColorDataArrayOffset[i]);
            }


            for (int j = 0; j < TEXTURE_ARRAY_MAX; j++) 
            {
                TexcoordDataArrayOffset[j] = BigEndian.ReadInt32(br);
                DataCountAdder(TexcoordDataArrayOffset[j]);
            }
            

            //VTX1のデータを取得
            VertexAttributeSet(br);
            J3DFileStreamSys.PaddingSkip(br);
            Console.WriteLine($"endpos: {br.BaseStream.Position.ToString("X")}");
            Console.WriteLine("VTX1 End");
            Console.WriteLine();
        }

        private List<VTX1DataAttributes> VTX1DataAttributesList;

        private void VertexAttributeSet(BinaryReader br) 
        {
            VTX1DataAttributesList = new List<VTX1DataAttributes>();
            Console.WriteLine($"VertexAttributeCount: {VertexAttributeCount-1}");
            for (int i = 0; i < VertexAttributeCount-1; i++) 
            {
                GXAttributeTypes GXAttrType = (GXAttributeTypes)BigEndian.ReadUInt32(br);
                var GXCompCount = BigEndian.ReadUInt32(br);
                Type ComponentType = null;
                if (GXAttrType == GXAttributeTypes.Color0 || GXAttrType == GXAttributeTypes.Color1)
                {
                    Console.WriteLine($"Color: {GXAttrType} {br.BaseStream.Position}");
                    ComponentType = GXColorTypes[BigEndian.ReadUInt32(br)];
                    
                }
                else 
                {
                    Console.WriteLine($"Data: {GXAttrType} {br.BaseStream.Position}");
                    ComponentType = GXDataTypes[BigEndian.ReadUInt32(br)];

                }

                VTX1DataAttributes vTX1DataAttributes = 
                    new VTX1DataAttributes(
                    GXAttrType,
                    GXCompCount,
                    ComponentType,
                    br.ReadByte()
                    );
                br.ReadBytes(3);
                VTX1DataAttributesList.Add(vTX1DataAttributes);
            }
                
        }

        private void DataCountAdder(int checArrayCount) 
        {
            if (checArrayCount > 0) VertexAttributeCount++;
        }

        private const byte COLOR_ARRAY_MAX = 2;
        private const byte TEXTURE_ARRAY_MAX = 8;

        public enum GXAttributeTypes : uint
        {
            PositionNormalMatrix     = 0x00000000,
            TextureMatrix0           = 0x00000001,
            TextureMatrix1           = 0x00000002,
            TextureMatrix2           = 0x00000003,
            TextureMatrix3           = 0x00000004,
            TextureMatrix4           = 0x00000005,
            TextureMatrix5           = 0x00000006,
            TextureMatrix6           = 0x00000007,
            TextureMatrix7           = 0x00000008,
            Position                 = 0x00000009,
            Normal                   = 0x0000000A,
            Color0                   = 0x0000000B,
            Color1                   = 0x0000000C,
            TextureCoord0            = 0x0000000D,
            TextureCoord1            = 0x0000000E,
            TextureCoord2            = 0x0000000F,
            TextureCoord3            = 0x00000010,
            TextureCoord4            = 0x00000011,
            TextureCoord5            = 0x00000012,
            TextureCoord6            = 0x00000013,
            TextureCoord7            = 0x00000014,
            PositionMatrixArray      = 0x00000015,
            NormalMatrixArray        = 0x00000016,
            TexMatrixArray           = 0x00000017,
            LightArray               = 0x00000018,
            NormalBinormalTangent    = 0x00000019,
            NullAttribute            = 0x000000FF
        }

        public static readonly Dictionary<uint, Type> GXDataTypes = new Dictionary<uint, Type>()
        {
            { 0x00000000 , typeof(byte) },
            { 0x00000001 , typeof(sbyte) },
            { 0x00000002 , typeof(ushort) },
            { 0x00000003 , typeof(short) },
            { 0x00000004 , typeof(float) }
        };

        public static readonly Dictionary<uint, Type> GXColorTypes = new Dictionary<uint, Type>()
        {
            { 0x00000000,typeof(RGB565)},
            { 0x00000001,typeof(RGB8)},
            { 0x00000002,typeof(RGBX8)},
            { 0x00000003,typeof(RGBA4)},
            { 0x00000004,typeof(RGBA6)},
            { 0x00000005,typeof(RGBA8)},
        };

        public struct VTX1DataAttributes
        {
            public GXAttributeTypes GXAttr;
            public uint GXCompCount;
            public Type GXCompType;
            public byte CompShift;
            public VTX1DataAttributes(GXAttributeTypes gxAttr, uint gxCompCount, Type gxCompType, byte compShift)
            {
                GXAttr = gxAttr;
                GXCompCount = gxCompCount;
                GXCompType = gxCompType;
                CompShift = compShift;
            }
        }
    }
}
