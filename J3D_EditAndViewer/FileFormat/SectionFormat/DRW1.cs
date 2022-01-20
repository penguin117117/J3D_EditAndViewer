using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.IO;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class DRW1
    {
        private string SectionName;
        private int SectionSize;
        private short DrawCount;
        private int MatrixArrayOffset;
        private int DataArrayOffset;

        public DRW1() 
        {
            
        }

        public void Read(BinaryReader br) 
        {
            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);
            DrawCount = BigEndian.ReadInt16(br);
            br.ReadBytes(2);
            MatrixArrayOffset = BigEndian.ReadInt32(br);
            DataArrayOffset = BigEndian.ReadInt32(br);

            var weightList = new List<byte>();
            var dataList = new List<short>();
            for (int i = 0; i < DrawCount; i++) 
            {
                weightList.Add(br.ReadByte());
            }

            for (int j = 0; j < DrawCount; j++)
            {
                dataList.Add(BigEndian.ReadInt16(br));
            }

            J3DFileStreamSys.PaddingSkip(br);
            Console.WriteLine($"DRW1 End: {br.BaseStream.Position.ToString("X")}");
        }
    }
}
