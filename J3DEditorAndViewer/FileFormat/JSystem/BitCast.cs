using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem
{
    internal class BitCast
    {
        public static uint ASCIIToInt(string value) => BinaryPrimitives.ReadUInt32BigEndian(Encoding.ASCII.GetBytes(value));
        public static string IntToASCII(uint value)
        {
            byte[] bufValue = new byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(bufValue, value);
            return Encoding.ASCII.GetString(bufValue);
        }
    }
}
