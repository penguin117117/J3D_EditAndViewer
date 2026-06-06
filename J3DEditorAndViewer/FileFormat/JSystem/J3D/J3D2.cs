using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.DRW;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.EVP;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.INF;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.JNT;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.MAT;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.MDL;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.TEX;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D
{
    public class J3D2
    {
        IHeader Header;

        public INF INF { get; private set; } = new();
        public VTX VTX { get; private set; } = new();
        public EVP EVP { get; private set; } = new();
        public DRW DRW { get; private set; } = new();
        public JNT JNT { get; private set; } = new();
        public SHP SHP { get; private set; } = new();
        public MAT MAT { get; private set; } = new();
        public MDL MDL { get; private set; } = new();
        public TEX TEX { get; private set; } = new();

        private readonly uint[] J3DSubSystem = [
            BitCast.ASCIIToInt("J3D1"),
            BitCast.ASCIIToInt("J3D2"),
        ];

        private readonly uint[] J3DSubType = {
            BitCast.ASCIIToInt("bdl4"),
            BitCast.ASCIIToInt("bmd3"),
        };

        // private readonly Dictionary<int, IModel_3D> Model3D_Dictionary = new Dictionary<string, IModel_3D>()
        // {
        //     { BitCast.ASCIIToInt("bdl4"), new BDL() },
        //     { BitCast.ASCIIToInt("bmd3"), new BDL() }
        // };

        private enum Support
        {
            Model,
            Animation,
            Unknown
        }

        private bool IsModel()
        {
            switch (Array.IndexOf(J3DSubSystem, Header.SubSystem))
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new Exception($"「{BitCast.IntToASCII(Header.SubSystem)}」はサポートされていません。");
            }
        }


        public J3D2()
        {
        }

        public void Read(EndianBinaryReaderBase br)
        {
            Read(new Header(br), br);
        }

        public void Read(IHeader header, EndianBinaryReaderBase br)
        {
            Header = header;
            if (!J3DSubSystem.Contains(Header.SubSystem))
            {
                throw new ArgumentException($"SubSystem: {BitCast.IntToASCII(Header.SubSystem)} がJ3DXではありません。");
            }
            if (!J3DSubType.Contains(Header.SubSystemType))
            {
                throw new Exception($"SubSystemType: {BitCast.IntToASCII(Header.SubSystemType)} はサポートされていません。");
            }

            for (int i = 0; i < header.SectionCount; i++)
            {
                ISectionHeader sectionHeader = new SectionHeader(br);
                if (INF.J3DSectionType.Contains(sectionHeader.Type))
                {
                    INF.Read(sectionHeader, br);
                    continue;
                }
                if (VTX.J3DSectionType.Contains(sectionHeader.Type))
                {
                    VTX.Read(sectionHeader, br, INF);
                    continue;
                }
                if (EVP.J3DSectionType.Contains(sectionHeader.Type))
                {
                    EVP.Read(sectionHeader, br);//, INF);
                    continue;
                }
                if (DRW.J3DSectionType.Contains(sectionHeader.Type))
                {
                    DRW.Read(sectionHeader, br);//, INF);
                    continue;
                }
                if (JNT.J3DSectionType.Contains(sectionHeader.Type))
                {
                    JNT.Read(sectionHeader, br);//, INF);
                    continue;
                }
                if (SHP.J3DSectionType.Contains(sectionHeader.Type))
                {
                    SHP.Read(sectionHeader, br);//, INF);
                    continue;
                }
                if (MAT.J3DSectionType.Contains(sectionHeader.Type))
                {
                    MAT.Read(sectionHeader, br);//, INF);
                    continue;
                }
                if (MDL.J3DSectionType.Contains(sectionHeader.Type))
                {
                    MDL.Read(sectionHeader, br);//, INF);
                    continue;
                }
                if (TEX.J3DSectionType.Contains(sectionHeader.Type))
                {
                    TEX.Read(sectionHeader, br);//, INF);
                    continue;
                }
            }

            if (br.BaseStream.Position > (Header.BeginPosition + Header.SizeInBytes))
            {
                throw new Exception("JSystemの整合性が取れません。");
            }

            br.BaseStream.Seek(header.BeginPosition + header.SizeInBytes, SeekOrigin.Begin);
            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
        }

        private void GetModel(FileStream fs)
        {
            // Model = Model3D_Dictionary[Header.SubSystemType];
            // Model.Read(fs);
            Console.WriteLine($"「{Header.SubSystemType}」ファイルです");
        }

        private void GetAnimation(FileStream fs)
        {
            Console.WriteLine("アニメーションファイルです");
        }
    }
}
