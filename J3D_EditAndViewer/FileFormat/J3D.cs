using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.FileFormat.Model_3D;
using J3D_EditAndViewer.IO;


namespace J3D_EditAndViewer.FileFormat
{

    public class J3D
    {
        public string Version { get; private set; }
        public string FileExtensionName { get; private set; }
        public int FileSize { get; private set; }
        public int SectionCount { get; private set; }
        public string SubVersion { get; private set; }

        public IModel_3D Model { get; private set; }

        private readonly string[] SupportJ3D_Ver = {
            "J3D1",
            "J3D2"
        };

        private readonly string[] SupportModel = {
            "bdl4",
            "bmd3"
        };

        private readonly Dictionary<string, IModel_3D> Model3D_Dictionary = new Dictionary<string, IModel_3D>() 
        {
            { "bmd3" , new BDL() },
            { "bdl4" , new BDL() }
        };

        private enum Support 
        {
            Model,
            Animation,
            Unknown
        }

        private bool IsModel() 
        {
            switch (Array.IndexOf(SupportJ3D_Ver, Version)) 
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new Exception($"「{Version}」はサポートされていません。");
            }
        }


        public J3D() 
        {
        
        }
        
        public void SetFromFile(FileStream fs) 
        {
            using (var br = new BinaryReader(fs))
            {
                Version = Encoding.ASCII.GetString(br.ReadBytes(4));
                FileExtensionName = Encoding.ASCII.GetString(br.ReadBytes(4));
                FileSize = BigEndian.ReadInt32(br);
                SectionCount = BigEndian.ReadInt32(br);
                SubVersion = Encoding.ASCII.GetString(br.ReadBytes(4));

                //パディングをスキップ
                br.ReadBytes(12);

                Console.WriteLine(Version);
                Console.WriteLine(FileExtensionName);
                Console.WriteLine(FileSize.ToString("X"));
                Console.WriteLine(SectionCount.ToString("X"));
                Console.WriteLine(SubVersion);


                if (IsModel()) GetModel(fs);
                else GetAnimation(fs);
                
            }
        }

        private void GetModel(FileStream fs) 
        {
            Model = Model3D_Dictionary[FileExtensionName];
            Model.Read(fs);
            Console.WriteLine($"「{ FileExtensionName }」ファイルです");
        }

        private void GetAnimation(FileStream fs) 
        {
            Console.WriteLine("アニメーションファイルです");
        }
    }
}
