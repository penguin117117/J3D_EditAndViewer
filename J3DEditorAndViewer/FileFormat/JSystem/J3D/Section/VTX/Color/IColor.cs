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
using System.Net.Http.Headers;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX.Color
{
    internal interface IColor
    {
        int SizeOfBytes { get; }
        Vector4i Read(EndianBinaryReaderBase br);

        private static int Regix(in int color, in int colorBit, in int regixBit)
        {
            // 例3bit。(この後のコメントも3bitを例とする。)
            // 0b101 = 0b10110110
            //       = (0b101 * 0b100000) | (0b101 * 0b100) | 0b10
            //       = ((0b101 * 0b1000000) | (0b101 * 0b1000) | 0b101) >> 1
            //       = ((0b101 * 0b1000000) | (0b101 * 0b1000) | 0b101) >> ((3[bit] * 3) - 8[bit])
            //       = ((0b101 * (0b1000 << 3)) | (0b101 * (0b1 << 3)) | 0b101) >> ((3[bit] * 3) - 8[bit])
            //       = ((0b101 * (0b1 << (3 * 2))) | (0b101 * (0b1 << (3 * 1))) | 0b101) >> ((3[bit] * 3) - 8[bit])
            //       = ((0b101 << (3 * 2)) | (0b101 << (3 * 1)) | 0b101) >> ((3[bit] * 3) - 8[bit])
            // 番号振り ((  4   << ( 2,3 )) | (        1       ) |   0  ) >> (         7           )

            // 0. 最初の時点では3bitを代入。
            // 1. 3bitをシフトして4~6bitを生成。
            // 2. 次のシフト先を計算。
            // 3. 次のシフト先が7bit目であるため続行。
            // 4. 6(3+3)bitシフトして7~9bitを生成。
            // 5. 次のシフト先を計算。
            // 6. 次のシフト先が10bit目であるためループを抜ける。
            // 7. 9bitの下1桁を削り8bitに正規化する。

            int regixser = colorBit; // 0
            int retColor = color;
            do
            {
                retColor |= (color << regixser); // 1, 4
                regixser += colorBit; // 2, 5
            } while (regixser < regixBit); // 3, 6

            return retColor >> (regixser - colorBit); // 7
        }
        static Vector4i Regix(in Vector4i value, in int redBit, in int greenBit, in int blueBit, in int alphaBit, in int regixBit = 8)
        {
            return new(
                Regix(value.X, redBit, regixBit),
                Regix(value.Y, greenBit, regixBit),
                Regix(value.Z, blueBit, regixBit),
                Regix(value.W, alphaBit, regixBit));

            //int redDiffBit = regixBit - redBit;
            //int greenDiffBit = regixBit - greenBit;
            //int blueDiffBit = regixBit - blueBit;
            //int alphaDiffBit = regixBit - alphaBit;

            //return new Vector4i(
            //    (value.X << redDiffBit) | (value.X >> (redBit - redDiffBit)),
            //    (value.Y << greenDiffBit) | (value.Y >> (greenBit - greenDiffBit)),
            //    (value.Z << blueDiffBit) | (value.Z >> (blueBit - blueDiffBit)),
            //    (value.W << alphaDiffBit) | (value.W >> (alphaBit - alphaDiffBit))
            //    );
        }

        public static IColor GetReader(EndianBinaryReaderBase br, in VertexAttribute vertexAttribute)
        {
            if (vertexAttribute.Type is not Types.GXAttribType.Color0
                                     and not Types.GXAttribType.Color1)
            {
                throw new ArgumentException();
            }

            return vertexAttribute.GXAttribPointerType switch
            {
                Types.GXAttribPointerType.RGB565 => new RGB565(),
                Types.GXAttribPointerType.RGB8 => new RGB8(),
                Types.GXAttribPointerType.RGBX8 => new RGBX8(),
                Types.GXAttribPointerType.RGBA4 => new RGBA4(),
                // Types.GXAttribPointerType.RGBA6 => new RGBA6(),
                Types.GXAttribPointerType.RGBA8 => new RGBA8(),
                _ => throw new ArgumentException()
            };
        }
    }
}
