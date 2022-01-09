using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using J3D_EditAndViewer.FileFormat;

namespace J3D_EditAndViewer.UI.MainWindowSys
{
    public class SceneTreeNodeView
    {
        public TreeView SceneTreeView { get; private set; }
        private readonly J3D _j3d;

        public SceneTreeNodeView(TreeView sceneTreeView,J3D j3d) 
        {
            SceneTreeView = sceneTreeView;
            _j3d = j3d;
        }

        public void SetTree()
        {
            //ツリービューの設定
            SceneTreeView.Nodes.Clear();
            var HierarchyDepth = _j3d.Model.SceneTreeData.HierarchyMaxDepth;
            List<List<byte>> TreePos = new List<List<byte>>();
            TreePos.Add(new List<byte>());

            //JointRootまたはWorldRootの設定
            var FirstNode = _j3d.Model.SceneTreeData.SceneDatas[0];
            TreeNode tn = SceneTreeView.Nodes.Add($"{FirstNode.NodeType}{FirstNode.NodeTypeTagNo}", $"{FirstNode.NodeType}{FirstNode.NodeTypeTagNo}", $"{FirstNode.HierarchyParentID.Item1}{FirstNode.HierarchyParentID.Item2}");

            byte oldindex = 0;
            foreach (var Scene in _j3d.Model.SceneTreeData.SceneDatas)
            {
                //初回のみ処理をスキップ
                if (Scene.HierarchyID == 0)
                {
                    continue;
                }

                //親ノードを検索し現在のノードを親ノードに変更します。
                var find = SceneTreeView.Nodes.Find($"{Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}", true);
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
                else if (oldindex > Scene.HierarchyID)
                {
                    //親ノードにノード追加
                    tn.Nodes.Add($"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.NodeType}{Scene.NodeTypeTagNo}", $"{Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}");
                }
                Console.WriteLine($"Parent: {Scene.HierarchyParentID.Item1}{Scene.HierarchyParentID.Item2}");
                oldindex = (byte)Scene.HierarchyID;

            }
            SceneTreeView.ExpandAll();
        }
    }
}
