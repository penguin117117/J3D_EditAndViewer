using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using J3D_EditAndViewer.IO;

namespace J3D_EditAndViewer.FileFormat.SectionFormat
{
    public class INF1
    {
        private string SectionName;
        private int SectionSize;
        private short UnknownFlag;
        private short UnknownParam;
        private int MatrixGroupCount;
        private int VertexCount;//vtx1セクションで使用する。
        private int HierarchyDataOffset;

        public TreeNode SceneTreeNode { get; private set; }
        public TreeView TreeView { get; set; }

        public List<SceneData> SceneDatas { get; private set; }
        public byte HierarchyMaxDepth { get; private set; }
        /// <summary>
        /// モデルの階層を示したShort型のデータ
        /// </summary>
        public enum NodeTypes : ushort
        {
            FinishNode = 0x0000,
            NewNode = 0x0001,
            EndNode = 0x0002,
            Joint = 0x0010,
            Material = 0x0011,
            Geometry = 0x0012
        }

       


        /// <summary>
        /// A structure that stores the data of the INF1 section.<br/>
        /// INF1セクションのデータを格納する構造体
        /// </summary>
        public struct SceneData
        {
            public ushort HierarchyID;
            public (NodeTypes,ushort) HierarchyParentID;
            public NodeTypes NodeType;
            public ushort NodeTypeTagNo;
            public SceneData(ushort hierarchyID, (NodeTypes, ushort) hierarchyParentID, NodeTypes nodeType, ushort nodeTypeTagNo)
            {
                HierarchyID = hierarchyID;
                HierarchyParentID = hierarchyParentID;
                NodeType = nodeType;
                NodeTypeTagNo = nodeTypeTagNo;
            }
        }

        public INF1() 
        {
            SceneDatas = new List<SceneData>();
            SceneTreeNode = new TreeNode();
        }

        public void Read(BinaryReader br) 
        {
            SectionName = Encoding.ASCII.GetString(br.ReadBytes(4));
            SectionSize = BigEndian.ReadInt32(br);
            UnknownFlag = BigEndian.ReadInt16(br);
            UnknownParam = BigEndian.ReadInt16(br);
            MatrixGroupCount = BigEndian.ReadInt32(br);
            VertexCount = BigEndian.ReadInt32(br);
            HierarchyDataOffset = BigEndian.ReadInt32(br);
            ReadSceneNodes(br);
            Debug();
        }

        private void ReadSceneNodes(BinaryReader br) 
        {
            
            ushort HierarchyID,NodeType,NodeTypeTagNo;
            HierarchyMaxDepth = 0;
            HierarchyID = 0;

            //NodeType = BigEndian.ReadUInt16(br);
            //NodeTypeTagNo = BigEndian.ReadUInt16(br);
            //SceneTreeNode.Text = $"{((NodeTypes)NodeType)}{NodeTypeTagNo}";
            Stack<(NodeTypes, ushort)> ParentNode = new Stack<(NodeTypes, ushort)>();
            Stack<TreeNode> tnStack = new Stack<TreeNode>();


            //var tn = new TreeNode();
            //tn.Nodes.Add($"{(NodeTypes)NodeType}{NodeTypeTagNo}");
            //tnStack.Push(tn);
            bool IsFirst = true;
            while (true) 
            {
                
                NodeType = BigEndian.ReadUInt16(br);
                NodeTypeTagNo = BigEndian.ReadUInt16(br);
                if (IsFirst)
                {
                    ParentNode.Push(((NodeTypes)NodeType, NodeTypeTagNo));
                    IsFirst = false;
                }


                switch (NodeType)
                {
                    case 0x00:

                        Console.WriteLine("HierarchyDepth: " + HierarchyMaxDepth);

                        return;
                    case 0x01:
                        //ParentNode.Push(((NodeTypes)NodeType, NodeTypeTagNo));
                        //tn.Nodes.Add($"{(NodeTypes)NodeType}{NodeTypeTagNo}");
                        HierarchyID++;
                        if (HierarchyID > HierarchyMaxDepth)
                            HierarchyMaxDepth++;
                        //ParentNode.Pop();
                        //ParentNode.Pop();
                        ParentNode.Push((SceneDatas.Last().NodeType, SceneDatas.Last().NodeTypeTagNo));
                        break;
                    case 0x02:
                        //ParentNode.Push(ParentNode.Peek());
                        //ParentNode.Push((SceneDatas.Last().NodeType, SceneDatas.Last().NodeTypeTagNo));
                        ParentNode.Pop();
                        
                        
                        //tn = tn.Parent;

                        //Console.WriteLine($"02: {tn.FullPath}");
                        HierarchyID--;
                        break;
                    case 0x10:
                    case 0x11:
                    case 0x12:

                        //if(ParentNode.Count <= 1)
                        //ParentNode.Pop();
                        SceneDatas.Add(new SceneData(HierarchyID, ParentNode.Peek(), (NodeTypes)NodeType, NodeTypeTagNo));
                        //ParentNode.Push(((NodeTypes)NodeType, NodeTypeTagNo));
                        

                        //ParentNode.Push(((NodeTypes)NodeType, NodeTypeTagNo));
                        break;
                }
            }
        }



        private void Debug() 
        {
            Console.WriteLine(SectionName);
            Console.WriteLine(SectionSize.ToString("X"));
            Console.WriteLine(UnknownFlag.ToString("X"));
            Console.WriteLine(UnknownParam.ToString("X"));
            Console.WriteLine(MatrixGroupCount.ToString("X"));
            Console.WriteLine(VertexCount.ToString("X"));
            Console.WriteLine(HierarchyDataOffset.ToString("X"));
        }
    }
}
