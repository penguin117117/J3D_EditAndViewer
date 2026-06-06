using System.Buffers.Binary;

namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryWriterSystem
{
    internal sealed class BigEndianBinaryWriter : EndianBinaryWriterBase
    {
        public override FileEndianType FileEndian => FileEndianType.Big; 

        internal BigEndianBinaryWriter(Stream stream) : base(stream) 
        {
        }

        public override void Write(byte[] buffer) => throw new NotSupportedException();
        public override void Write(Half value) => throw new NotSupportedException();
        public override void Write(char[] chars, int index, int count) => throw new NotSupportedException();
        public override void Write(decimal value) => throw new NotSupportedException();
        public override void Write(ReadOnlySpan<byte> buffer) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int index, int count)=> throw new NotSupportedException();
        public override void Write(ReadOnlySpan<char> chars) => throw new NotSupportedException();

        public override void Write(short value)
        {
            Span<byte> buffer = stackalloc byte[sizeof(short)];
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(int value)
        {
            Span<byte> buffer = stackalloc byte[sizeof(int)];
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(long value)
        {
            Span<byte> buffer = stackalloc byte[sizeof(long)];
            BinaryPrimitives.WriteInt64BigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(ushort value) 
        {
            Span<byte> buffer = stackalloc byte[sizeof(ushort)];
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(uint value)
        {
            Span<byte> buffer = stackalloc byte[sizeof(uint)];
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(ulong value)
        {
            Span<byte> buffer = stackalloc byte[sizeof(ulong)];
            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(float value) 
        {
            Span<byte> buffer = stackalloc byte[sizeof(float)];
            BinaryPrimitives.WriteSingleBigEndian(buffer, value);
            OutStream.Write(buffer);
        }

        public override void Write(double value)
        {
            Span<byte> buffer = stackalloc byte[sizeof(double)];
            BinaryPrimitives.WriteDoubleBigEndian(buffer, value);
            OutStream.Write(buffer);
        }
    } 
}
