using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util
{
    /// <summary>
    /// JSystme の読み込み時に便利な機能を提供する静的なクラスです。
    /// </summary>
    internal static class JSystemReadUtil
    {
        /// <summary>
        /// パディングの読み込みをスキップする機能を提供します。
        /// </summary>
        internal static class PaddingSkip
        {
            private const int AlighmentSize_32Byte = 32;
            private const int AlighmentSize_16Byte = 16;
            private const int AlighmentSize_08Byte = 8;

            /// <summary>
            /// 32Byteで調整されたパディングをスキップします。
            /// </summary>
            /// <param name="ebrb"></param>
            internal static void Alignment32Byte(EndianBinaryReaderBase ebrb)
            {
                while (true)
                {
                    if (ebrb.BaseStream.Position % AlighmentSize_32Byte == 0) break;
                    ebrb.ReadByte();
                }
            }

            /// <summary>
            /// 16Byteで調整されたパディングをスキップします。
            /// </summary>
            /// <param name="ebrb"></param>
            internal static void Alignment16Byte(EndianBinaryReaderBase ebrb)
            {
                while (true)
                {
                    if (ebrb.BaseStream.Position % AlighmentSize_16Byte == 0) break;
                    ebrb.ReadByte();
                }
            }

            /// <summary>
            /// 8Byteで調整されたパディングをスキップします。
            /// </summary>
            /// <param name="ebrb"></param>
            internal static void Alignment8Byte(EndianBinaryReaderBase ebrb)
            {
                while (true)
                {
                    if (ebrb.BaseStream.Position % AlighmentSize_08Byte == 0) break;
                    ebrb.ReadByte();
                }
            }
        }




    }
}
