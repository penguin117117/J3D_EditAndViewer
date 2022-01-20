using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class EVP1
    {
        private long BaseAddress;
        private string SectionName;
        private int SectionSize;
        private short EnvelopeMatrixCount;
        private int JointCountTableOffset;
        private int IndexTableOffset;
        private int WeightTableOffset;
        private int InverseBindPoseTableOffset;

        public struct IndexData 
        {
            public short Parent;
            public short Child;
            public IndexData(short parent,short child) 
            {
                Parent = parent;
                Child = child;
            }
        }

        public struct WeightData 
        {
            public float Weight1;
            public float Weight2;
            public WeightData(float weight1,float weight2) 
            {
                Weight1 = weight1;
                Weight2 = weight2;
            }
        }

        public struct EnvelopeMatrixData 
        {
            public short Index;
            public float Weigth;
            public Matrix4 Matrix4;
            public EnvelopeMatrixData(short index,float weight , Matrix4 matrix4) 
            {
                Index = index;
                Weigth = weight;
                Matrix4 = matrix4;
            }
        }

        public EVP1() 
        {
        
        }

        public void Read(BinaryReader br) 
        {
            BaseAddress = br.BaseStream.Position;
            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);
            EnvelopeMatrixCount = BigEndian.ReadInt16(br);

            //パディングスキップ
            BigEndian.ReadInt16(br);

            JointCountTableOffset        = BigEndian.ReadInt32(br);
            IndexTableOffset             = BigEndian.ReadInt32(br);
            WeightTableOffset            = BigEndian.ReadInt32(br);
            InverseBindPoseTableOffset   = BigEndian.ReadInt32(br);

            int TotalMatrix = 0;
            for (int MatrixIndex = 0; MatrixIndex < EnvelopeMatrixCount; MatrixIndex++) 
            {
                TotalMatrix += br.ReadByte();
            }

            //var JointCountTable = br.ReadBytes(EnvelopeMatrixCount);
            var IndexTabe = new List<short>();
            var WeightTable = new List<float>();

            for (int i = 0; i < TotalMatrix; i++) 
            {
                IndexTabe.Add(BigEndian.ReadInt16(br));
            }
            Console.WriteLine($"index End: {br.BaseStream.Position.ToString("X")}");
            for (int j = 0; j < TotalMatrix; j++)
            {
                WeightTable.Add(BigEndian.ReadFloat(br));
            }
            Console.WriteLine($"weight End: {br.BaseStream.Position.ToString("X")}");
            //List<(byte, float)> IndexTable = new List<(byte, float)>();
            //for (int i = 0; i< EnvelopeMatrixCount; i++) 
            //    IndexTable.Add((br.ReadByte(), BigEndian.ReadFloat(br)));

            List<Matrix4> matrix4List = new List<Matrix4>();
            for (int k = 0; k < TotalMatrix; k++) 
            {
                var row0 = new Vector4(BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br));
                //var row1 = new Vector4(BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br));
                //var row2 = new Vector4(BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br));
                //var row3 = new Vector4(BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br), BigEndian.ReadFloat(br));
                //Matrix4 matrix4 = new Matrix4(row0,row1,row2, row3);
            }

            J3DFileStreamSys.PaddingSkip(br);

            Console.WriteLine($"ENV1 End: {br.BaseStream.Position.ToString("X")}");
        }
    }
}
