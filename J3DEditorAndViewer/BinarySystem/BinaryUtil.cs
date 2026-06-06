using System.Diagnostics;
using System.Text;

namespace GalaxyPlantInCrystal_KEIJI.IO.BinarySystem
{
    public static class BinaryUtil
    {
        /// <summary>
        /// JSystemのパディングをスキップします
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="alighmentSize"></param>
        public static void PaddingSkip(Stream stream , float alighmentSize = 32f)
        {
            if (stream.Position % alighmentSize != 0)
            {
                while (true)
                {
                    if (stream.Position % alighmentSize == 0) break;
                    stream.Position++;
                }
            }
        }

        public static long GetPaddingReaderCount(long pos) 
        {
            long addPos = 0;
            if (pos % 32f != 0)
            {
                while (true)
                {
                    if (pos % 32f == 0) break;
                    pos++;
                    addPos++;
                }
            }
            return addPos;
        }

        public static string ReadStringNullEnd(BinaryReader br , long nameoffset , bool isLastItem = false) {
            var oldpos = br.BaseStream.Position;

            var strPos = br.BaseStream.Seek(oldpos + nameoffset, SeekOrigin.Begin);

            Debug.WriteLine(strPos.ToString("X"));

            List<byte> stringBytes = new();
            while (true) 
            {
                stringBytes.Add(br.ReadByte());
                if (stringBytes[^1] == 0x00) 
                {
                    break;
                }
            }

            if(!isLastItem)
            br.BaseStream.Seek(oldpos, SeekOrigin.Begin);

            var bitarray = stringBytes.ToArray();
            var str = Encoding.GetEncoding(65001).GetString(bitarray) ;

            return str[0..^1];
        }

        public static string ReadStringNullEnd(BinaryReader br) 
        {
            List<byte> stringBytes = new();
            while (true)
            {
                stringBytes.Add(br.ReadByte());
                if (stringBytes[^1] == 0x00)
                {
                    break;
                }
            }

            var bitarray = stringBytes.ToArray();

            var nameString = Encoding.GetEncoding(65001).GetString(bitarray);

            return nameString[0..^1];
        }

        /// <summary>
        /// <see href="seekStringOffset"/> で指定した場所までSeekを行い文字列をNull末端まで取得します。<br/>
        /// その後Seek直前の位置まで現在のストリーム位置をSeekします。
        /// </summary>
        /// <param name="br"></param>
        /// <param name="seekStringOffset"></param>
        /// <returns></returns>
        public static string ReadSeekStringNullEndAndSeekFix(BinaryReader br, long seekStringOffset) 
        {
            long fixPosition = br.BaseStream.Position;

            //ファイル名の取得
            br.BaseStream.Seek(seekStringOffset, SeekOrigin.Begin);
            string dirName = ReadStringNullEnd(br);
            //Debug.WriteLine("StringEndPosition: " + br.BaseStream.Position.ToString("X"));

            //ストリームの位置を元に戻す
            br.BaseStream.Seek(fixPosition, SeekOrigin.Begin);

            return dirName;
        }

        /// <summary>
        /// <paramref name="bw"/>で指定された<see cref="BinaryWriter"/>に、BigEndianでUint32のデータを書き込みます。
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="writeData"></param>
        public static void WriteUint32BigEndian(BinaryWriter bw, uint writeData) 
        {
            if (!BitConverter.IsLittleEndian) 
            {
                bw.Write(BitConverter.GetBytes(writeData));
                return;
            }

            bw.Write(BitConverter.GetBytes(writeData).Reverse().ToArray());
        }

        /// <summary>
        /// <paramref name="bw"/>で指定された<see cref="BinaryWriter"/>に、BigEndianでInt32のデータを書き込みます。
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="writeData"></param>
        public static void WriteInt32BigEndian(BinaryWriter bw, int writeData)
        {
            if (!BitConverter.IsLittleEndian)
            {
                bw.Write(BitConverter.GetBytes(writeData));
                return;
            }

            bw.Write(BitConverter.GetBytes(writeData).Reverse().ToArray());
        }

        /// <summary>
        /// <paramref name="bw"/>で指定された<see cref="BinaryWriter"/>に、BigEndianでInt16のデータを書き込みます。
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="writeData"></param>
        public static void WriteInt16BigEndian(BinaryWriter bw, short writeData)
        {
            if (!BitConverter.IsLittleEndian)
            {
                bw.Write(BitConverter.GetBytes(writeData));
                return;
            }

            bw.Write(BitConverter.GetBytes(writeData).Reverse().ToArray());
        }

        /// <summary>
        /// <paramref name="bw"/>で指定された<see cref="BinaryWriter"/>に、BigEndianでUInt16のデータを書き込みます。
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="writeData"></param>
        public static void WriteUInt16BigEndian(BinaryWriter bw, ushort writeData)
        {
            if (!BitConverter.IsLittleEndian)
            {
                bw.Write(BitConverter.GetBytes(writeData));
                return;
            }

            bw.Write(BitConverter.GetBytes(writeData).Reverse().ToArray());
        }

        /// <summary>
        /// ファイルに4バイトのNullデータを書き込みます。
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="writeRepeatCount"><see cref="int"/>型のNullデータを書き込む回数</param>
        public static void NullWrite4Byte(BinaryWriter bw, int writeRepeatCount = 1)
        {

            //0以上または、int型の最大値までしか選択できないようにする。
            if (writeRepeatCount < 1)
            {
                writeRepeatCount = 1;
            }

            if (int.MaxValue < writeRepeatCount) 
            {
                writeRepeatCount = int.MaxValue;
            }

            for (int i = 0; i < writeRepeatCount; i++) 
            {
                bw.Write(BitConverter.GetBytes(0x00000000));
            }
                
        }

        public static void PaddingWriter(BinaryWriter bw)
        {
            if (bw.BaseStream.Position % 32f != 0)
            {
                while (bw.BaseStream.Position % 32f != 0)
                {
                    bw.BaseStream.WriteByte(0x00);
                }
            }
        }

        public static void WriteASCIIString(BinaryWriter bw, string writeString) 
        {
            bw.Write(Encoding.ASCII.GetBytes(writeString));
        }

        //TODO: Null末端まで読み込む処理の追加が必要
        public static string ReadUTF16BE_NullEnd(BinaryReader br, int readchers = 2)
        {
            string strs = "";
            //ここで、発生するエラーは過去に既存のMSBTで編集したデータを使用している可能性が高い
            try
            {
                strs = Encoding.GetEncoding("unicodeFFFE").GetString(br.ReadBytes(readchers));
            }
            catch (ArgumentException age) 
            {
                MessageBox.Show("古いバージョンのMSBTエディタで編集されたデータがけんちされました");
            }

            return strs;
        }
    }
}
