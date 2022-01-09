using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace J3D_EditAndViewer.IO
{
    public static class BigEndian
    {
        private const byte Int32Size = 4;
        private const byte Int16Size = 2;
        private const byte OffByOne = 1;

        public static ushort ReadUInt16(BinaryReader br)
        {
            return BitConverter.ToUInt16(ReadReverse2Byte(br), 0);
        }

        public static uint ReadUInt32(BinaryReader br)
        {
            return BitConverter.ToUInt32(ReadReverse4Byte(br), 0);
        }

        public static short ReadInt16(BinaryReader br)
        {
            return BitConverter.ToInt16(ReadReverse2Byte(br), 0);
        }

        /// <summary>
        /// BigEndianのInt32型でバイナリファイルを読み込みます。
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static int ReadInt32(BinaryReader br) 
        {
            return BitConverter.ToInt32(ReadReverse4Byte(br),0);
        }

        private static byte[] ReadReverse2Byte(BinaryReader br)
        {
            byte[] ReverseBytes = new byte[Int16Size];
            for (int i = 0; i < Int16Size; i++)
            {
                ReverseBytes[Int16Size - OffByOne - i] = br.ReadByte();
            }
            return ReverseBytes;
        }

        private static byte[] ReadReverse4Byte(BinaryReader br) 
        {
            byte[] ReverseBytes = new byte[Int32Size];
            for (int i = 0; i < Int32Size; i++)
            {
                ReverseBytes[Int32Size - OffByOne - i] = br.ReadByte();
            }
            return ReverseBytes;
        }





        
    }
}
