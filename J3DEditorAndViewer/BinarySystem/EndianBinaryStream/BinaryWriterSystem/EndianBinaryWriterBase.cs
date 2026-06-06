using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryWriterSystem
{
    internal class EndianBinaryWriterBase:BinaryWriter,IFileEndianable
    {
        public virtual FileEndianType FileEndian => FileEndianType.None;

        public EndianBinaryWriterBase(Stream stream): base (stream, Encoding.UTF8, false)
        {

        }

        
    }
}
