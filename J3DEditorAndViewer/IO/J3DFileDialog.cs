// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using J3DEditorAndViewer.FileFormat;
using J3DEditorAndViewer.FileFormat.Model_3D;
using J3DEditorAndViewer.FileFormat.SectionFormat;
using J3DEditorAndViewer.FileFormat.JSystem.J3D;
using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;


namespace J3DEditorAndViewer.IO
{
    public class J3DFileDialog
    {
        private string _openFilePath;
        private string _InitDirectryPath;
        private readonly string _filter = "BDL(*.bdl)|*.bdl|すべてのファイル(*.*)|*.*";
        //public J3DHeader J3DHeaderData{ get; private set; }
        //public TreeView treeView { get; private set; }
        //public TreeNode _tn { get; private set; }
        //private TreeNode tn;
        public J3D J3DData { get; private set; }
        public J3D2 J3DData2 { get; private set; }


        public J3DFileDialog()
        {
            //treeView = tv;
        }

        public void Open()
        {
            DialogResult dialogResult;
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "J3DModel.bdl",
                InitialDirectory = GetInitialDirectory(),
                Filter = _filter,
                FilterIndex = 1,
                Title = "開きたいファイル選択"
            })
            {
                dialogResult = ofd.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    Properties.Settings.Default.Path = Path.GetDirectoryName(ofd.FileName);
                    Properties.Settings.Default.Save();
                    _openFilePath = ofd.FileName;
                    OpenFileStream();

                }
            }
        }

        private void OpenFileStream()
        {
            J3DData = new J3D();
            J3DData2 = new J3D2();
            using (FileStream fs = new FileStream(_openFilePath, FileMode.Open))
            {
                J3DData.SetFromFile(fs);
            }
            using (FileStream fs = new(_openFilePath, FileMode.Open))
            {
                using EndianBinaryReaderBase br = new BigEndianBinaryReader(fs);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                J3DData2.Read(br);
            }
        }


        private string GetInitialDirectory()
        {
            var path = Properties.Settings.Default.Path;
            if (Directory.Exists(path) && path != string.Empty)
                return path;
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        }
    }

}
