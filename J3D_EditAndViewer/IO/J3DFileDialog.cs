using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using J3D_EditAndViewer.FileFormat;
using J3D_EditAndViewer.FileFormat.Model_3D;
using J3D_EditAndViewer.FileFormat.SectionFormat;


namespace J3D_EditAndViewer.IO
{
    public class J3DFileDialog
    {
        private string _openFilePath;
        private string _InitDirectryPath;
        private readonly string _filter = "BDL(*.bdl)|*.bdl|すべてのファイル(*.*)|*.*";
        //public J3DHeader J3DHeaderData{ get; private set; }
        public TreeView treeView { get; private set; }
        public TreeNode _tn { get; private set; }
        //private TreeNode tn;

        public J3DFileDialog(TreeView tv) 
        {
            treeView = tv;
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

                    using (FileStream fs = new FileStream(_openFilePath,FileMode.Open)) 
                    {
                        J3D j3d = new J3D();
                        j3d.SetFromFile(fs);



                        //ツリービューの設定
                        treeView.Nodes.Clear();
                        var HierarchyDepth = j3d.Model.SceneTreeData.HierarchyMaxDepth;
                        List<List<byte>> TreePos = new List<List<byte>>();
                        TreePos.Add(new List<byte>());

                        //JointRootまたはWorldRootの設定
                        var FirstNode = j3d.Model.SceneTreeData.SceneDatas[0];
                        TreeNode tn = treeView.Nodes.Add($"{FirstNode.NodeType}{FirstNode.NodeTypeTagNo}", $"{FirstNode.NodeType}{FirstNode.NodeTypeTagNo}", $"{FirstNode.HierarchyParentID.Item1}{FirstNode.HierarchyParentID.Item2}");
                        
                        byte oldindex = 0;
                        foreach (var Scene in j3d.Model.SceneTreeData.SceneDatas)
                        {
                            //初回のみ処理をスキップ
                            if (Scene.HierarchyID == 0) 
                            {
                               continue;
                            }

                            //親ノードを検索し現在のノードを親ノードに変更します。
                            var find = treeView.Nodes.Find($"{Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}", true);
                            tn = find[0];

                            if (oldindex == Scene.HierarchyID) 
                            {
                                //ノードを追加
                                tn.Nodes.Add($"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}");
                            }
                            else if (oldindex < Scene.HierarchyID)
                            {
                                //子ノードを作成しノード追加
                                tn = tn.Nodes.Add($"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}");
                            }
                            else if(oldindex > Scene.HierarchyID)
                            {
                                //親ノードにノード追加
                                tn.Nodes.Add($"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}");
                            }
                            Console.WriteLine($"Parent: {Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}");
                            oldindex = (byte)Scene.HierarchyID;

                        }


                        treeView.ExpandAll();
                    }
                }
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

        private readonly string[] SupportJ3D_Ver = { "J3D2" };

        
     
    }
   
}
