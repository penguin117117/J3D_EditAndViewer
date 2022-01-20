using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.IO;
using OpenTK;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class JNT1
    {
        private long BaseAddress;
        private string SectionName;
        private int SectionSize;
        private short JointCount;
        private int JointTransformTableOffset;
        private int RemapTableOffset;
        private int NameTableOffset;
        private List<JointTransformationData> jointTransformationData;

        public struct JointTransformationData
        {
            public short MatrixType;
            public bool HasNotScaleFromParents;
            public Vector3 Scale;
            public Vector3h Rotation;
            public Vector3 Translation;
            public float BoundingSphereRadius;
            public Vector3 BoundingBoxMin;
            public Vector3 BoundingBoxMax;
        }

        public JNT1()
        {
            jointTransformationData = new List<JointTransformationData>();
        }

        public void Read(BinaryReader br) 
        {
            BaseAddress = br.BaseStream.Position;
            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);
            JointCount = BigEndian.ReadInt16(br);
            br.ReadBytes(2);
            JointTransformTableOffset = BigEndian.ReadInt32(br);
            RemapTableOffset = BigEndian.ReadInt32(br);
            NameTableOffset = BigEndian.ReadInt32(br);

            Console.WriteLine($"JointCount: {JointCount.ToString("X")}");

            for (int i = 0; i<JointCount; i++) 
            {
                var jointdata = new JointTransformationData();

                jointdata.MatrixType = BigEndian.ReadInt16(br);
                jointdata.HasNotScaleFromParents = br.ReadBoolean();
                br.ReadByte();
                jointdata.Scale = J3DFileStreamSys.ReadFloatVector3(br);
                jointdata.Rotation = J3DFileStreamSys.ReadShortVector3(br);
                br.ReadBytes(2);
                jointdata.Translation = J3DFileStreamSys.ReadFloatVector3(br);
                jointdata.BoundingSphereRadius = BigEndian.ReadFloat(br);
                jointdata.BoundingBoxMin = J3DFileStreamSys.ReadFloatVector3(br);
                jointdata.BoundingBoxMax = J3DFileStreamSys.ReadFloatVector3(br);

                jointTransformationData.Add(jointdata);
            }

            Console.WriteLine($"JNT1 types End: {br.BaseStream.Position.ToString("X")}");

            for (int j = 0; j<JointCount; j++)
            {
                BigEndian.ReadInt16(br);
            }

            Console.WriteLine($"JNT1 remap End: {br.BaseStream.Position.ToString("X")}");

            //8バイトに揃えます
            var jointCountByte = JointCount * 2;
            while (jointCountByte%8 != 0) 
            {
                br.ReadBytes(2);
                jointCountByte += 2;
            }

            //以下文字列テーブルの処理
            var StringCount = BigEndian.ReadInt16(br);
            br.ReadBytes(2);

            for (int k = 0; k<StringCount; k++) 
            {
                var hash = BigEndian.ReadInt16(br);
                var offset = BigEndian.ReadInt16(br);
            }

            for (int m = 0; m < StringCount; m++)
            {
                string str = string.Empty;
                List<byte> bytes = new List<byte>();
                
                //ボーン名を取得します
                while (true) 
                {
                    var bit = br.ReadByte();
                    
                    if (bit == 0x00) 
                    {
                        
                        break;
                    }
                    bytes.Add(bit);
                }
                Console.WriteLine(Encoding.ASCII.GetString(bytes.ToArray()));
            }

            J3DFileStreamSys.PaddingSkip(br);
            Console.WriteLine($"JNT1 End: {br.BaseStream.Position.ToString("X")}");
        }

        
    }

    
}
