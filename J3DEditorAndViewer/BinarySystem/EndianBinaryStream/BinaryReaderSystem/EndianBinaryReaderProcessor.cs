using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem
{
    public class EndianBinaryReaderProcessor
    {
        public EndianBinaryReaderBase Reader { get; private set; }

        protected enum FileHeaders : uint
        {
            RARC = 0x52415243,
            CRAR = 0x43524152
        }

        protected static readonly Dictionary<uint, FileEndianType> keyValuePairs = new()
        {
            { (uint)FileHeaders.RARC, FileEndianType.Big},
                { (uint)FileHeaders.CRAR ,FileEndianType.Little}
        };


        public EndianBinaryReaderProcessor(Stream stream)
        {
            Reader = new BigEndianBinaryReader(stream);
            var endianByte = Reader.ReadUInt32();

            

            switch (keyValuePairs[endianByte])
            {
                case FileEndianType.Big:
                    stream.Seek(0, SeekOrigin.Begin);
                    Reader = new BigEndianBinaryReader(stream);
                    break;
                case FileEndianType.Little:
                    stream.Seek(0, SeekOrigin.Begin);
                    Reader = new LittleEndianBinaryReader(stream);
                    break;
                default:
                    throw new FileFormatException($"{endianByte:X8} はエンディアンのパラメータではありません");

            }

        }
    }


}
