using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryWriterSystem
{
    internal class EndianBinaryWriterProcessor : IDisposable
    {
        public EndianBinaryWriterBase Writer { get; private set; }

        public EndianBinaryWriterProcessor(Stream stream,FileEndianType fileEndianType) 
        {
            switch (fileEndianType) 
            {
                case FileEndianType.Big:
                    Writer = new BigEndianBinaryWriter(stream);
                    break;
                case FileEndianType.Little:
                    Writer = new LittleEndianBinaryWriter(stream);
                    break;
                case FileEndianType.None:
                    throw new FileFormatException($"{nameof(EndianBinaryWriterProcessor)}に対して{FileEndianType.None}は無効な値です。");
                default:
                    throw new FileFormatException($"未知のエンディアンタイプが入力されました。{FileEndianType.Big}または、{FileEndianType.Little}以外使用できません。");

            }
        }

        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}
