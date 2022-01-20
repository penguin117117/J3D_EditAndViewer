using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.IO;
using OpenTK;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class SHP1
    {
        private string SectionName;
        private int SectionSize;
        private short ShapeCount;

        private int ShapeDataOffset;
        private int RemapTabelOffset;
        private int NameTableOffset;
        private int AttributeTableOffset;
        private int MatrixTableOffset;
        private int PrimitiveDataOffset;
        private int MatrixDataOffset;
        private int MatrixGroupTableOffset;

        public struct ShapeData 
        {
            public short DisplayFlags;
            public short MatrixGroupCount;
            public short AttributesOffset;
            public short FirstMatrixDataIndex;
            public short FirstMatrixGroupIndex;

            public float BoundingSphereRadius;
            public Vector3 BoundingBoxMin;
            public Vector3 BoundingBoxMax;
        }

        public struct VertexAttributes 
        {
            public int AttributeType;
            public int DataType;
        }

        public SHP1() 
        {
        
        }

        public void Read(BinaryReader br) 
        {
            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);
            ShapeCount = BigEndian.ReadInt16(br);
            br.ReadBytes(2);

            ShapeDataOffset = BigEndian.ReadInt32(br);
            RemapTabelOffset = BigEndian.ReadInt32(br);
            NameTableOffset = BigEndian.ReadInt32(br);
            AttributeTableOffset = BigEndian.ReadInt32(br);
            MatrixGroupTableOffset = BigEndian.ReadInt32(br);
            PrimitiveDataOffset = BigEndian.ReadInt32(br);
            MatrixDataOffset = BigEndian.ReadInt32(br);
            MatrixGroupTableOffset = BigEndian.ReadInt32(br);

            for (int i = 0; i < ShapeCount; i++)
            {
                var shapeData = new ShapeData();
                shapeData.DisplayFlags = BigEndian.ReadInt16(br);
                shapeData.MatrixGroupCount = BigEndian.ReadInt16(br);
                shapeData.AttributesOffset = BigEndian.ReadInt16(br);
                shapeData.FirstMatrixDataIndex = BigEndian.ReadInt16(br);
                shapeData.FirstMatrixGroupIndex = BigEndian.ReadInt16(br);
                br.ReadBytes(2);
                shapeData.BoundingSphereRadius = BigEndian.ReadFloat(br);
                shapeData.BoundingBoxMin = J3DFileStreamSys.ReadFloatVector3(br);
                shapeData.BoundingBoxMax = J3DFileStreamSys.ReadFloatVector3(br);
            }

            for (int j = 0; j < ShapeCount; j++) 
            {
                var reNameMap = BigEndian.ReadInt16(br);
            }

            J3DFileStreamSys.PaddingSkip(br);

            //Vertex Attributes を取得
            //var NullAttributeCount = ShapeCount;
            //while (NullAttributeCount > 0) 
            //{
            //    var vtxAttr = new VertexAttributes();
            //    vtxAttr.AttributeType = BigEndian.ReadInt32(br);
            //    vtxAttr.DataType = BigEndian.ReadInt32(br);
            //    if (vtxAttr.AttributeType == 0x000000FF) 
            //    {
            //        NullAttributeCount--;
            //    }
            //}

            

            

            Console.WriteLine($"SHP1 End: {br.BaseStream.Position.ToString("X")}");

        }

        //StartPos & SHP1BaseAddress 0x20E0
        /*
        //SHP1 ヘッダー情報
        53 48 50 31 //SHP1
        00 00 2A E0 //SectionSize
        00 02       //ShapeCount
        FF FF       //Padding
        00 00 00 2C //ShapeDataOffset
        00 00 00 7C //RemapTableOffset
        00 00 00 00 //NameTabelOffset
        00 00 00 80 //AttributeTableOffset
        00 00 00 D8 //MatrixTableOffset
        00 00 01 20 //PrimitiveDataOffset
        00 00 2A A0 //MatrixDataOffset
        00 00 2A C0 //MatrixGroupTableOffset
         */
        //Endpos 0x210B

        //StartPos 0x210C
        /*
        //シェイプデータ01
        03 FF       //DisplayFlags
        00 03       //MatrixGroupCount
        00 00       //AttributesOffset
        00 00       //FirstMatrixDataIndex
        00 00       //FirstMatrixGroupIndex
        FF FF       //Padding
        43 48 98 23 //BoundingSphereRadius
        C3 3F A8 DC //BBoxMinX
        C2 90 1A 24 //BBoxMinY
        C2 07 8C 73 //BBoxMinZ
        43 3F A8 DC //BBoxMaxX
        42 82 F8 79 //BBoxMaxY
        42 29 6F D0 //BBoxMaxZ

        //シェイプデータ02
        00 FF 
        00 01 
        00 30 
        00 03 
        00 03 
        FF FF 
        42 27 46 70 
        C1 F0 C2 EE 
        41 09 88 95 
        40 86 FD E9 
        41 F0 C2 EE 
        41 F7 4D 6A 
        41 CC A2 0F
         */
        //EndPos:  0x215B

        //StartPos: 0x215C
        /*
        //リネームテーブル
        00 00 00 01
         */
        //EndPos: 0x215F

        //StartPos: 0x2160
        /*
         
        00 00 00 00 00 00 00 01 
        00 00 00 09 00 00 00 03 
        00 00 00 0A 00 00 00 03 
        00 00 00 0B 00 00 00 03 
        00 00 00 0D 00 00 00 03 
        00 00 00 FF 00 00 00 00 
        
        00 00 00 09 00 00 00 03 
        00 00 00 0A 00 00 00 03 
        00 00 00 0B 00 00 00 03 
        00 00 00 0D 00 00 00 03 
        00 00 00 FF 00 00 00 00
         */
        //EndPos: 0x21B7

        //StartPos: 0x21B8
        /*
        //
        00 04
        00 00 00 0A 00 01 00 0F 00 06 00 09 00 03 00
        02 00 08 FF FF 00 0C 00 0B FF FF 00 0D 00 0E 
        00 05 00 07 FF FF FF FF 00 06 00 11 00 10 FF 
        FF 00 12 00 13 FF FF FF FF FF FF FF FF 00 00
         */



        /*
        12 00 0F 00 21 00 19 00 21 
        12 00 10 00 22 00 0C 00 22 
        1B 00 0C 00 1C 00 05 00 1C 
        1B 00 0D 00 1D 00 0C 00 1D 
        09 00 09 00 17 00 13 00 17 
        09 00 0A 00 18 00 0C 00 18 
        09 00 06 00 12 00 0F 00 12 
        09 00 07 00 13 00 0C 00 13 
        09 00 04 00 0E 00 0C 00 0E 
        09 00 23 00 42 00 1B 00 3E 
        09 00 21 00 3F 00 01 00 3C 
        09 00 22 00 40 00 2A 00 3D 
        09 00 20 00 3C 00 08 00 3B 
        09 00 B9 00 41 00 2B 00 10 
        09 00 BB 00 3D 00 08 00 06 
        09 01 12 00 D4 00 0B 00 3B 
        09 00 C7 00 3E 00 09 00 09 
        09 01 10 00 D5 00 29 00 3A 
        09 00 C8 00 3A 00 28 00 01 
        09 01 11 00 D3 00 27 00 0C 
        09 01 4D 00 A6 00 02 00 02 
        09 01 50 01 13 00 01 00 97 
        09 00 F3 00 AC 00 29 00 0C
        
        00 0F 00 21 00 19 00 21 12 
        00 10 00 22 00 0C 00 22 1B 
        00 0C 00 1C 00 05 00 1C 1B 
        00 0D 00 1D 00 0C 00 1D 09 
        00 09 00 17 00 13 00 17 09 
        00 0A 00 18 00 0C 00 18 09 
        00 06 00 12 00 0F 00 12 09 
        00 07 00 13 00 0C 00 13 09 
        00 04 00 0E 00 0C 00 0E 09 
        00 23 00 42 00 1B 00 3E 09 
        00 21 00 3F 00 01 00 3C 09 
        00 22 00 40 00 2A 00 3D 09 
        00 20 00 3C 00 08 00 3B 09 
        00 B9 00 41 00 2B 00 10 09 
        00 BB 00 3D 00 08 00 06 09 
        01 12 00 D4 00 0B 00 3B 09 
        00 C7 00 3E 00 09 00 09 09 
        01 10 00 D5 00 29 00 3A 09 
        00 C8 00 3A 00 28 00 01 09 
        01 11 00 D3 00 27 00 0C 09 
        01 4D 00 A6 00 02 00 02 09 
        01 50 01 13 00 01 00 97 09 
        00 F3 00 AC 00 29 00 0C
         */

    }
}
