using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.IO;
using J3D_EditAndViewer.FileFormat.SectionFormat.VTX1ColorData;
using System.IO.Compression;
using J3D_EditAndViewer.FileFormat.SectionFormat.VTX1PrimData;
using OpenTK;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class VTX1
    {
        private long SectionBaseAddres;

        private string SectionName;
        private int SectionSize;
        private int VertexFormatOffset;
        private int PositionDataArrayOffset;
        private int NormalDataArrayOffset;
        private int NBT_DataArrayOffset;
        private int[] ColorDataArrayOffset;
        private int[] TexcoordDataArrayOffset;

        private int _inf1VertexCount;

        //private byte VertexAttributeCount;
        private List<(bool,int)> IsReadArray;

        public List<Vector3> Position { get; private set; }
        public List<Vector3> Normal { get; private set; }
        public List<Vector3> Dummy { get; private set; }

        public List<byte[]> BytesList;

        //private List<string, int> aaa;

        public byte[] PositionBytes { get; private set; }
        public byte[] NormalBytes { get; private set; }
        public byte[] NBTDataBytes { get; private set; }
        public byte[][] ColorDataBytes { get; private set; }
        public byte[][] TexturerCoordBytes { get; private set; }

        public VTX1()
        {
            Position = new List<Vector3>();
            Normal = new List<Vector3>();
            Dummy = new List<Vector3>();

            BytesList = new List<byte[]>();
            //BytesArray.Add(new List<byte>());

            IsReadArray = new List<(bool,int)>();
            ColorDataArrayOffset = new int[COLOR_ARRAY_MAX];
            TexcoordDataArrayOffset = new int[TEXTURE_ARRAY_MAX];

            ColorDataBytes = new byte[COLOR_ARRAY_MAX][];
            TexturerCoordBytes = new byte[TEXTURE_ARRAY_MAX][];
        }
        public void Read(BinaryReader br, int vertexCount)
        {
            Console.WriteLine("VTX1 Start");

            _inf1VertexCount = vertexCount;

            SectionBaseAddres = br.BaseStream.Position;

            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);

            VertexFormatOffset = BigEndian.ReadInt32(br);


            //PositionDataArrayOffset = BigEndian.ReadInt32(br);
            //NormalDataArrayOffset = BigEndian.ReadInt32(br);
            //NBT_DataArrayOffset = BigEndian.ReadInt32(br);

            ////DataCountAdder(VertexFormatOffset);
            //DataCountAdder(PositionDataArrayOffset);
            //DataCountAdder(NormalDataArrayOffset);
            //DataCountAdder(NBT_DataArrayOffset);

            //for (int i = 0; i < COLOR_ARRAY_MAX; i++)
            //{
            //    ColorDataArrayOffset[i] = BigEndian.ReadInt32(br);
            //    DataCountAdder(ColorDataArrayOffset[i]);
            //}


            //for (int j = 0; j < TEXTURE_ARRAY_MAX; j++)
            //{
            //    TexcoordDataArrayOffset[j] = BigEndian.ReadInt32(br);
            //    DataCountAdder(TexcoordDataArrayOffset[j]);
            //}


            for (int i = 0; i < 13; i++) 
            {
                SetOffsetPositionAndIsRead(br);
            }
            

            //VTX1のデータを取得
            VertexAttributeSet(br);
            J3DFileStreamSys.PaddingSkip(br);

            IsReadArray.Add((false, (int)SectionBaseAddres + SectionSize));

            //pos
            ReadArrays(br);

            Console.WriteLine($"endpos: {br.BaseStream.Position.ToString("X")}");
            Console.WriteLine("VTX1 End");
            Console.WriteLine();
        }

        public List<VTX1DataAttributes> VTX1DataAttributesList{ get; private set; }

        private void ReadArrays(BinaryReader br) 
        {
            
            

            //var IsReadData = IsReadArray.Where(x => x.Item1 == true).ToList();

            int NextPosIndex = 1; 
            for (int i = 0; i < IsReadArray.Count - 1; i++)
            {
                Console.WriteLine("Start read pos" + br.BaseStream.Position.ToString("X"));
                if (SectionBaseAddres + SectionSize == IsReadArray[i].Item2) break;
                if (SectionBaseAddres == IsReadArray[i].Item2) continue;
                //読み込みフラグがfalseなら読み込まない
                if (IsReadArray[i].Item1 == false)
                {
                    BytesList.Add(null);
                    continue;
                }

                var CurrentPosition  = IsReadArray[i].Item2;
                var NextPosition     = IsReadArray[i + NextPosIndex].Item2;

                //NextPositionがSectionBaseAddres以下の場合計算できないので判別する
                if (IsReadArray[i + NextPosIndex].Item2 == SectionBaseAddres) 
                {
                    //NextPositionが0よりも大きい場合を取得
                    while (true) 
                    {
                        NextPosIndex++;
                        NextPosition = IsReadArray[i + NextPosIndex].Item2;
                        if (NextPosition > SectionBaseAddres) break;
                    }
                }
                Console.WriteLine("★Position Data");
                Console.WriteLine(NextPosition.ToString("X"));
                Console.WriteLine(CurrentPosition.ToString("X"));
                var readbytes = NextPosition - CurrentPosition;
                Console.WriteLine("readbytes: " +readbytes.ToString("X"));
                BytesList.Add(br.ReadBytes(readbytes));
                //パディングも含めたバイト配列を取得する
                //for (long j = CurrentPosition; j < NextPosition; j++) 
                //{

                //}

            }

            Console.WriteLine(br.BaseStream.Position.ToString("X"));
            return;

            foreach (var vtx1 in VTX1DataAttributesList.Select((Value,Index)=> (Value, Index))) 
            {
                


                Console.WriteLine($"{vtx1.Value.GXAttr}: " + br.BaseStream.Position.ToString("X"));
                if (vtx1.Value.GXAttr == GXAttributeTypes.NullAttribute) break;
                
                if (vtx1.Value.GXAttr == GXAttributeTypes.Color0 || vtx1.Value.GXAttr == GXAttributeTypes.Color0)
                {
                    for (int i = 0; i < _inf1VertexCount; i++)
                        GXColorTypes[vtx1.Value.GXCompType].GetColor(br);
                }
                else if (vtx1.Value.GXAttr == GXAttributeTypes.Position)
                {

                    for (int j = 0; j < _inf1VertexCount; j++)
                    {
                        Position.Add(GXDataTypes[vtx1.Value.GXCompType].Set(br));
                    }

                }
                else if (vtx1.Value.GXAttr == GXAttributeTypes.Normal)
                {
                    Console.WriteLine($"Normal Coune: {(IsReadArray[vtx1.Index + 1].Item2 - IsReadArray[vtx1.Index].Item2).ToString("X")}");
                    Console.WriteLine($"{GXDataTypes[vtx1.Value.GXCompType]}");
                    var nextofsset = IsReadArray[vtx1.Index + 1].Item2;
                    if (nextofsset == 0) nextofsset = IsReadArray[vtx1.Index + 2].Item2;

                    var roopMax =  nextofsset - IsReadArray[vtx1.Index].Item2;
                    roopMax = ((roopMax) / 2) / 3;
                    Console.WriteLine($"roopMax: {roopMax.ToString("X")}");
                    for (int j = 0; j < roopMax /*_iNF1_VertexCount*/; j++)
                    {
                        Normal.Add(GXDataTypes[vtx1.Value.GXCompType].Set(br));
                        //Console.WriteLine(br.BaseStream.Position.ToString("X"));
                    }
                    
                }
                else 
                {
                    for (int j = 0; j < _inf1VertexCount; j++)
                    {
                        Dummy.Add(GXDataTypes[vtx1.Value.GXCompType].Set(br));
                    }
                }

                    
                J3DFileStreamSys.PaddingSkip(br);
                
            }
        }

        private void SetOffsetPositionAndIsRead(BinaryReader br) 
        {
            var ReadInt32 = BigEndian.ReadInt32(br);
            if (ReadInt32 > 0) 
            {
                IsReadArray.Add((true,(int)SectionBaseAddres + ReadInt32));
                return;
            }

            IsReadArray.Add((false,(int)SectionBaseAddres + ReadInt32));
        }

        /// <summary>
        /// NullAttributeを計算に入れるための定数
        /// </summary>
        const byte NullAttributeIncrement = 1;


        private void VertexAttributeSet(BinaryReader br)
        {
            VTX1DataAttributesList = new List<VTX1DataAttributes>();

            var IsReadPos = IsReadArray.Where(IsRead => IsRead.Item1 == true);

            //NullAttributeは計算に含まれないので辻褄合わせのための定数を使用
            for (int i = 0; i < IsReadPos.Count() + NullAttributeIncrement; i++)
            {
                var GXAttrType = (GXAttributeTypes)BigEndian.ReadUInt32(br);
                var GXCompCount = BigEndian.ReadUInt32(br);
                var GXAttr = BigEndian.ReadUInt32(br);

                var vTX1DataAttributes =
                        new VTX1DataAttributes(
                        GXAttrType,
                        GXCompCount,
                        GXAttr,
                        br.ReadByte()
                        );

                //パディングをスキップ
                br.ReadByte();
                br.ReadByte();
                br.ReadByte();

                VTX1DataAttributesList.Add(vTX1DataAttributes);
            }

            Console.WriteLine($"VertexAttributeCount: {VTX1DataAttributesList.Count}");
        }



        //private void DataCountAdder(int checArrayCount)
        //{
        //    if (checArrayCount > 0) 
        //    { 
        //        VertexAttributeCount++;
        //    }
        //}


        
        //関数はここまで

        //以下　定数　列挙配列　辞書　構造体宣言　などの不変の物
        private const byte COLOR_ARRAY_MAX = 2;
        private const byte TEXTURE_ARRAY_MAX = 8;

        public enum GXAttributeTypes : uint
        {
            PositionNormalMatrix = 0x00000000,
            TextureMatrix0 = 0x00000001,
            TextureMatrix1 = 0x00000002,
            TextureMatrix2 = 0x00000003,
            TextureMatrix3 = 0x00000004,
            TextureMatrix4 = 0x00000005,
            TextureMatrix5 = 0x00000006,
            TextureMatrix6 = 0x00000007,
            TextureMatrix7 = 0x00000008,
            Position = 0x00000009,
            Normal = 0x0000000A,
            Color0 = 0x0000000B,
            Color1 = 0x0000000C,
            TextureCoord0 = 0x0000000D,
            TextureCoord1 = 0x0000000E,
            TextureCoord2 = 0x0000000F,
            TextureCoord3 = 0x00000010,
            TextureCoord4 = 0x00000011,
            TextureCoord5 = 0x00000012,
            TextureCoord6 = 0x00000013,
            TextureCoord7 = 0x00000014,
            PositionMatrixArray = 0x00000015,
            NormalMatrixArray = 0x00000016,
            TexMatrixArray = 0x00000017,
            LightArray = 0x00000018,
            NormalBinormalTangent = 0x00000019,
            NullAttribute = 0x000000FF
        }

        public enum ArrayName :byte
        {
            Position = 0,
            Normal,
            NBT,
            Color1,
            Color2,
            TexCoord1,
            TexCoord2,
            TexCoord3,
            TexCoord4,
            TexCoord5,
            TexCoord6,
            TexCoord7,
            TexCoord8
        }

        public static Dictionary<uint, IPrim> GXDataTypes = new Dictionary<uint, IPrim>()
        {
            { 0x00000000 , new PrimByte() },
            { 0x00000001 , new PrimSByte() },
            { 0x00000002 , new PrimUShort() },
            { 0x00000003 , new PrimShort() },
            { 0x00000004 , new PrimFloat() }
        };

        public static readonly Dictionary<uint, IVertexColors> GXColorTypes = new Dictionary<uint, IVertexColors>()
        {
            { 0x00000000,new RGB565()},
            { 0x00000001,new RGB8()},
            { 0x00000002,new RGBX8()},
            { 0x00000003,new RGBA4()},
            { 0x00000004,new RGBA6()},
            { 0x00000005,new RGBA8()},
        };

        public struct VTX1DataAttributes
        {
            public GXAttributeTypes GXAttr;
            public uint GXCompCount;
            public uint GXCompType;
            public byte CompShift;
            public VTX1DataAttributes(GXAttributeTypes gxAttr, uint gxCompCount, uint gxCompType, byte compShift)
            {
                GXAttr = gxAttr;
                GXCompCount = gxCompCount;
                GXCompType = gxCompType;
                CompShift = compShift;
            }
        }

        public struct VTX1ColorAttributes
        {
            public GXAttributeTypes GXAttr;
            public uint GXCompCount;
            public IPrim GXCompType;
            public byte CompShift;
            public VTX1ColorAttributes(GXAttributeTypes gxAttr, uint gxCompCount, IPrim gxCompType, byte compShift)
            {
                GXAttr = gxAttr;
                GXCompCount = gxCompCount;
                GXCompType = gxCompType;
                CompShift = compShift;
            }
        }
    }
}
