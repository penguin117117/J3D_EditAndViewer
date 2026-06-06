// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP
{
    public class ShapeData
    {
        public enum MatrixType
        {
            SingleMatrix = 0x00,
            BillBoard = 0x01,
            YBillboard = 0x02,
            MultiMatrix = 0x03, // Skinned Model
        }

        public MatrixType Type { get; init; }
        public List<VertexIndexAttribute> UsedAttributesInPrimitive;

        public int TableCount { get; init; }

        // public Matrix[] Matrices { get; init; }
        public Primitive[] Primitives { get; init; }

        public float BoundingSphereRadius { get; init; }
        public Vector3 BoundingBoxMin { get; init; }
        public Vector3 BoundingBoxMax { get; init; }

        public ShapeData(EndianBinaryReaderBase br, in long beginPos, in SHPDataHeader sHPDataHeader, PrimitiveTable[] primitiveTables)
        {
            br.BaseStream.Seek(beginPos + sHPDataHeader.ShapeDataOffset, SeekOrigin.Begin);

            Type = (MatrixType)br.ReadByte();
            br.ReadByte(); // padding
            TableCount = br.ReadInt16();

            // Matrices = new Matrix[TableCount];
            Primitives = new Primitive[TableCount];
            int attributeOffset = br.ReadInt16();
            int tableMatrixBeginIndex = br.ReadInt16();
            int tablePrimitiveBeginIndex = br.ReadInt16();
            br.ReadInt16(); // padding

            BoundingSphereRadius = br.ReadSingle();
            BoundingBoxMin = new(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            BoundingBoxMax = new(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            UsedAttributesInPrimitive = VertexIndexAttribute.Read(br, beginPos, sHPDataHeader);
            // for (int i = 0; i < TableCount; i++) { }
            for (int i = 0; i < TableCount; i++)
            {
                Primitives[i] = new(br, beginPos, UsedAttributesInPrimitive, sHPDataHeader, primitiveTables[i + tablePrimitiveBeginIndex]);
            }
        }

        public static ShapeData[] Read(EndianBinaryReaderBase br, in long beginPos, in int shapeLength, in SHPDataHeader sHPDataHeader)
        {
            PrimitiveTable[] primitiveTables = PrimitiveTable.Read(br, beginPos, sHPDataHeader);

            ShapeData[] retValue = new ShapeData[shapeLength];
            for (int i = 0; i < shapeLength; i++)
            {
                retValue[i] = new(br, beginPos, sHPDataHeader, primitiveTables);
            }

            return retValue;
        }
    }
}
