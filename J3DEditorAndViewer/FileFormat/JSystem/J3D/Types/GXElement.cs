using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types
{
    // gpu index decode mode でも使用されます。
    [StructLayout(LayoutKind.Sequential)]
    public struct GXElement
    {
        // drw1 data offset?
        public int PositionMatrix = -1;
        // texture transform animation?
        public int TextureMatrix0 = -1;
        public int TextureMatrix1 = -1;
        public int TextureMatrix2 = -1;
        public int TextureMatrix3 = -1;
        public int TextureMatrix4 = -1;
        public int TextureMatrix5 = -1;
        public int TextureMatrix6 = -1;
        public int TextureMatrix7 = -1;

        public int Position = -1;
        // normal, binormal, tangent.
        public int NBT = -1;
        public int Color0 = -1;
        public int Color1 = -1;

        // texture coordinate.
        public int TextureCoordinate0 = -1;
        public int TextureCoordinate1 = -1;
        public int TextureCoordinate2 = -1;
        public int TextureCoordinate3 = -1;
        public int TextureCoordinate4 = -1;
        public int TextureCoordinate5 = -1;
        public int TextureCoordinate6 = -1;
        public int TextureCoordinate7 = -1;


        public Span<int> TexMatrix => MemoryMarshal.CreateSpan(ref TextureMatrix0, 8);
        public Span<int> Color => MemoryMarshal.CreateSpan(ref Color0, 8);
        public Span<int> TexCoord => MemoryMarshal.CreateSpan(ref Color0, 8);

        public GXElement() { }

        public void GetElements(EndianBinaryReaderBase br, VertexIndexAttribute attrib)
        {
            switch (attrib.AttributeType)
            {
                case GXAttribType.PositionMatrix: PositionMatrix = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix0: TexMatrix[0] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix1: TexMatrix[1] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix2: TexMatrix[2] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix3: TexMatrix[3] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix4: TexMatrix[4] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix5: TexMatrix[5] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix6: TexMatrix[6] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexMatrix7: TexMatrix[7] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.Position: Position = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.NBT: NBT = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.Color0: Color[0] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.Color1: Color[1] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord0: TexCoord[0] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord1: TexCoord[1] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord2: TexCoord[2] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord3: TexCoord[3] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord4: TexCoord[4] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord5: TexCoord[5] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord6: TexCoord[6] = attrib.Reader.ReadInt32(br); return;
                case GXAttribType.TexCoord7: TexCoord[7] = attrib.Reader.ReadInt32(br); return;
            }
        }
    }
}
