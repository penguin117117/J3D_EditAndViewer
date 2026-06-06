using System.Text;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem
{
    public abstract class EndianBinaryReaderBase : BinaryReader, IFileEndianable
    {
        public virtual FileEndianType FileEndian => FileEndianType.None;

        public EndianBinaryReaderBase(Stream stream) : base(stream, Encoding.UTF8, false)
        {

        }


    }


}
