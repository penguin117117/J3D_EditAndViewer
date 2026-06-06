using System.Buffers.Binary;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem
{
    public class BigEndianBinaryReader : EndianBinaryReaderBase
    {
        public override FileEndianType FileEndian => FileEndianType.Big;

        public BigEndianBinaryReader(Stream stream) : base(stream)
        {

        }

        public override short ReadInt16() => BinaryPrimitives.ReadInt16BigEndian(ReadBytes(2));
        public override int ReadInt32() => BinaryPrimitives.ReadInt32BigEndian(ReadBytes(4));
        public override long ReadInt64() => BinaryPrimitives.ReadInt64BigEndian(ReadBytes(8));
        public override float ReadSingle() => BinaryPrimitives.ReadSingleBigEndian(ReadBytes(4));
        public override double ReadDouble() => BinaryPrimitives.ReadDoubleBigEndian(ReadBytes(8));
        public override ushort ReadUInt16() => BinaryPrimitives.ReadUInt16BigEndian(ReadBytes(2));
        public override uint ReadUInt32() => BinaryPrimitives.ReadUInt32BigEndian(ReadBytes(4));
        public override ulong ReadUInt64() => BinaryPrimitives.ReadUInt64BigEndian(ReadBytes(8));

    }


}
