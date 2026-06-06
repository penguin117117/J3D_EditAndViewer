using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types.GXAttribPointerIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP
{
    public class VertexIndexAttribute
    {
        public GXAttribType AttributeType { get; private set; }
        public IGXAttribPointerIO Reader { get; private set; }

        public VertexIndexAttribute(GXAttribType attributeType, GXAttribPointerType gXAttribPointerType)
        {
            AttributeType = attributeType;
            Reader = IGXAttribPointerIO.GetIO(gXAttribPointerType);
        }
        public static List<VertexIndexAttribute> Read(EndianBinaryReaderBase br, in long beginPos, in SHPDataHeader sHPSectionHeader)
        {
            var header = sHPSectionHeader.ArrayDataPair[SHPDataHeader.Key.VertexIndexAttributes];

            br.BaseStream.Seek(beginPos + header.Offset, SeekOrigin.Begin);
            List<VertexIndexAttribute> attributes = new();

            while (true)
            {
                var Type = (GXAttribType)br.ReadUInt32();
                if (Type is GXAttribType.Null)
                {
                    break;
                }

                var GXAttribPointerType = (GXAttribPointerType)br.ReadInt32();
                JSystemReadUtil.PaddingSkip.Alignment8Byte(br);
                attributes.Add(new(Type, GXAttribPointerType));
            }

            return attributes;
        }
    }
}
