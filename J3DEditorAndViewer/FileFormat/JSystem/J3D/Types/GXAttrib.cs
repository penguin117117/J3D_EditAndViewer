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

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Types
{
    internal static class GXAttrib
    {
        public static int GetLengthOfGXCompType(EndianBinaryReaderBase br, GXAttribType gXAttribType)
        {
            int gXComponentCountTag = br.ReadInt32();

            switch (gXAttribType)
            {
                case GXAttribType.PositionMatrix:
                    return PositionMatrixCount(gXComponentCountTag);
                case GXAttribType.TexMatrix0:
                case GXAttribType.TexMatrix1:
                case GXAttribType.TexMatrix2:
                case GXAttribType.TexMatrix3:
                case GXAttribType.TexMatrix4:
                case GXAttribType.TexMatrix5:
                case GXAttribType.TexMatrix6:
                case GXAttribType.TexMatrix7:
                    return TexMatrixCount(gXComponentCountTag);

                case GXAttribType.Position:
                    return PositionCount(gXComponentCountTag);

                case GXAttribType.NBT: // GXAttribType.Normal
                case GXAttribType.NBT0:
                    return NBTCount(gXComponentCountTag);

                case GXAttribType.Color0:
                case GXAttribType.Color1:
                    return ColorCount(gXComponentCountTag);

                case GXAttribType.TexCoord0:
                case GXAttribType.TexCoord1:
                case GXAttribType.TexCoord2:
                case GXAttribType.TexCoord3:
                case GXAttribType.TexCoord4:
                case GXAttribType.TexCoord5:
                case GXAttribType.TexCoord6:
                case GXAttribType.TexCoord7:
                    return TexCoordCount(gXComponentCountTag);

                default:
                    throw new NotImplementedException($"未定義のGXAttribTypeが指定されました。 :{gXAttribType}");
            }
        }

        private static int PositionMatrixCount(int gXComponentCountTag)
        {
            return gXComponentCountTag switch
            {
                _ => throw new NotImplementedException(),
            };
        }
        private static int TexMatrixCount(int gXComponentCountTag)
        {
            return gXComponentCountTag switch
            {
                _ => throw new NotImplementedException(),
            };
        }
        private static int PositionCount(int gXComponentCountTag)
        {
            return gXComponentCountTag switch
            {
                0 => 2, // XY
                1 => 3, // XYZ
                _ => throw new NotImplementedException(),
            };
        }
        private static int NBTCount(int gXComponentCountTag)
        {
            return gXComponentCountTag switch
            {
                0 => 3,     // XYZ: Normal.
                // 1=> // XYZ: Normal, (Binormal, Tangent)?
                // 2=> // XYZ: Normal, Binormal, Tangent?
                _ => throw new NotImplementedException(),
            };
        }
        private static int ColorCount(int gXComponentCountTag)
        {
            return gXComponentCountTag switch
            {
                0 => 3, // RGB.
                1 => 4, // RGBA.
                _ => throw new NotImplementedException(),
            };
        }
        private static int TexCoordCount(int gXComponentCountTag)
        {
            return gXComponentCountTag switch
            {
                1 => 2, // XY.
                0 => 0,// X.
                _ => throw new NotImplementedException(),
            };
        }
    }
}
