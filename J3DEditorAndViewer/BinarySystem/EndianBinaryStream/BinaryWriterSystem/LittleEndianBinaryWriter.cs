namespace GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryWriterSystem
{
    internal sealed class LittleEndianBinaryWriter: EndianBinaryWriterBase 
    {
        public override FileEndianType FileEndian => FileEndianType.Little;

        internal LittleEndianBinaryWriter(Stream stream) : base(stream) 
        {
        
        }

        //規定でリトルエンディアンのため、オーバーライドは行わない。
    }
}
