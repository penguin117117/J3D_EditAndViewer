using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP
{
    public class Primitive
    {
        public GXPrimitiveType Type { get; private set; }

        public bool EnablePositionMatrix = false;
        public bool EnableTexMatrix0 = false;
        public bool EnableTexMatrix1 = false;
        public bool EnableTexMatrix2 = false;
        public bool EnableTexMatrix3 = false;
        public bool EnableTexMatrix4 = false;
        public bool EnableTexMatrix5 = false;
        public bool EnableTexMatrix6 = false;
        public bool EnableTexMatrix7 = false;
        public bool EnablePosition = false;
        public bool EnableNBT = false;
        public bool EnableColor0 = false;
        public bool EnableColor1 = false;
        public bool EnableTexCoord0 = false;
        public bool EnableTexCoord1 = false;
        public bool EnableTexCoord2 = false;
        public bool EnableTexCoord3 = false;
        public bool EnableTexCoord4 = false;
        public bool EnableTexCoord5 = false;
        public bool EnableTexCoord6 = false;
        public bool EnableTexCoord7 = false;

        public GXElement[] Elements { get; private set; }

        public Primitive(EndianBinaryReaderBase br, in long beginPos, in List<VertexIndexAttribute> attributes, in SHPDataHeader sHPDataHeader, in PrimitiveTable primitiveTable)
        {
            var offset = beginPos + sHPDataHeader.ArrayDataPair[SHPDataHeader.Key.Primitives].Offset;
            br.BaseStream.Seek(offset + primitiveTable.Offset, SeekOrigin.Begin);

            Type = (GXPrimitiveType)br.ReadByte();
            Elements = new GXElement[br.ReadInt16()];

            for (int i = 0; i < Elements.Length; i++)
            {
                Elements[i] = new GXElement();
                foreach (var attribute in attributes)
                {
                    switch (attribute.AttributeType)
                    {
                        case GXAttribType.PositionMatrix: EnablePositionMatrix = true; break;
                        case GXAttribType.TexMatrix0: EnableTexMatrix0 = true; break;
                        case GXAttribType.TexMatrix1: EnableTexMatrix1 = true; break;
                        case GXAttribType.TexMatrix2: EnableTexMatrix2 = true; break;
                        case GXAttribType.TexMatrix3: EnableTexMatrix3 = true; break;
                        case GXAttribType.TexMatrix4: EnableTexMatrix4 = true; break;
                        case GXAttribType.TexMatrix5: EnableTexMatrix5 = true; break;
                        case GXAttribType.TexMatrix6: EnableTexMatrix6 = true; break;
                        case GXAttribType.TexMatrix7: EnableTexMatrix7 = true; break;
                        case GXAttribType.Position: EnablePosition = true; break;
                        case GXAttribType.NBT: EnableNBT = true; break;
                        case GXAttribType.Color0: EnableColor0 = true; break;
                        case GXAttribType.Color1: EnableColor1 = true; break;
                        case GXAttribType.TexCoord0: EnableTexCoord0 = true; break;
                        case GXAttribType.TexCoord1: EnableTexCoord1 = true; break;
                        case GXAttribType.TexCoord2: EnableTexCoord2 = true; break;
                        case GXAttribType.TexCoord3: EnableTexCoord3 = true; break;
                        case GXAttribType.TexCoord4: EnableTexCoord4 = true; break;
                        case GXAttribType.TexCoord5: EnableTexCoord5 = true; break;
                        case GXAttribType.TexCoord6: EnableTexCoord6 = true; break;
                        case GXAttribType.TexCoord7: EnableTexCoord7 = true; break;
                    }
                    Elements[i].GetElements(br, attribute);
                }
            }

            if ((br.BaseStream.Position) > (offset + primitiveTable.Offset + primitiveTable.LengthInByte))
            {
                throw new Exception();
            }
        }
    }
}
