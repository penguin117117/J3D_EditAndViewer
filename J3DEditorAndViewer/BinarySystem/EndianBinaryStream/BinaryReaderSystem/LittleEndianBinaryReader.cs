using System.Buffers.Binary;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem
{
    public class LittleEndianBinaryReader : EndianBinaryReaderBase
    {
        public override FileEndianType FileEndian => FileEndianType.Little;

        public LittleEndianBinaryReader(Stream stream) : base(stream)
        {

        }

        public override short ReadInt16() => BinaryPrimitives.ReadInt16LittleEndian(ReadBytes(2));
        public override int ReadInt32() => BinaryPrimitives.ReadInt32LittleEndian(ReadBytes(4));
        public override long ReadInt64() => BinaryPrimitives.ReadInt64LittleEndian(ReadBytes(8));
        public override float ReadSingle() => BinaryPrimitives.ReadSingleLittleEndian(ReadBytes(4));
        public override double ReadDouble() => BinaryPrimitives.ReadDoubleLittleEndian(ReadBytes(8));
        public override ushort ReadUInt16() => BinaryPrimitives.ReadUInt16LittleEndian(ReadBytes(2));
        public override uint ReadUInt32() => BinaryPrimitives.ReadUInt32LittleEndian(ReadBytes(4));
        public override ulong ReadUInt64() => BinaryPrimitives.ReadUInt64LittleEndian(ReadBytes(8));

    }


}
