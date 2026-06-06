using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.INF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.INF
{
    public class INF : IINFVertex
    {
        private ISectionHeader Header;
        public short UnknownFlag { get; private set; }
        public short UnknownParam { get; private set; }
        public int MatrixGroupCount { get; private set; }
        public int VertexCount { get; private set; }
        public int HierarchyDataOffset { get; private set; }

        /// <summary>
        /// Listは美松にオブジェクトを追加することがMS規定書に書かれています。
        /// https://learn.microsoft.com/ja-jp/dotnet/api/system.collections.generic.list-1.add?view=net-8.0#system-collections-generic-list-1-add(-0)
        /// 上記の仕様に依存するため、ソートなどで順番の変更をしてはならない。
        /// </summary>
        public List<Hierarchy> Hierarchies { get; private set; }

        public readonly uint[] J3DSectionType = [
           BitCast.ASCIIToInt($"{nameof(INF)}1"),
        ];
        public INF() { }

        public void Read(EndianBinaryReaderBase br)
        {
            Read(new SectionHeader(br), br);
        }
        public void Read(ISectionHeader header, EndianBinaryReaderBase br)
        {
            Header = header;

            if (!J3DSectionType.Contains(Header.Type))
            {
                throw new ArgumentException($"Type: {BitCast.IntToASCII(Header.Type)} がINFXではありません。");
            }

            UnknownFlag = br.ReadInt16();
            UnknownParam = br.ReadInt16();
            MatrixGroupCount = br.ReadInt32();
            VertexCount = br.ReadInt32();
            HierarchyDataOffset = br.ReadInt32();

            ReadHierarchys(br);

            if (br.BaseStream.Position > Header.BeginPosition + Header.SizeInBytes)
            {
                throw new Exception("JSystem.Sectionの整合性が取れません。");
            }


            br.BaseStream.Seek(header.BeginPosition + header.SizeInBytes, SeekOrigin.Begin);
            JSystemReadUtil.PaddingSkip.Alignment32Byte(br);
        }

        private void ReadHierarchys(EndianBinaryReaderBase br)
        {
            br.BaseStream.Seek(HierarchyDataOffset + Header.BeginPosition, SeekOrigin.Begin);
            Hierarchies = new();
            int depthCheck = 0;
            while (true)
            {
                Hierarchies.Add(new(br));
                var last = Hierarchies.Last();
                depthCheck += last.IsStepNest();

                if (last.IsFinishd())
                {
                    break;
                }
            }
            if (depthCheck != 0)
            {
                throw new Exception("不正な階層構造です。");
            }
        }



        public void WriteHierarchies(TreeNodeCollection treeNodeCollection)
        {
            Stack<TreeNodeCollection> currentTNC = new();
            TreeNodeCollection? previousTNC = null;

            currentTNC.Push(treeNodeCollection);

            treeNodeCollection.Clear();
            for (int i = 0; i < Hierarchies.Count; i++)
            {
                var hierarchie = Hierarchies[i];

                switch (hierarchie.Type)
                {
                    case Hierarchy.NodeType.FinishNode:
                        if (i != Hierarchies.Count - 1)
                        {
                            throw new Exception("階層データが壊れています。");
                        }
                        return;
                    case Hierarchy.NodeType.NewNode:
                        if (previousTNC is null)
                        {
                            throw new NullReferenceException("階層データが壊れています。");
                        }
                        currentTNC.Push(previousTNC);
                        break;
                    case Hierarchy.NodeType.EndNode:
                        previousTNC = currentTNC.Pop();
                        break;
                    default:
                        previousTNC = currentTNC.Peek().Add(
                            $"{hierarchie.Type}{hierarchie.ManageID}",
                            $"{hierarchie.Type}{hierarchie.ManageID}",
                            $"{hierarchie.Type}{hierarchie.ManageID}").Nodes;
                        break;
                }
            }
            throw new Exception("階層データが壊れています。(終端ノードが存在しません。)");
        }
    }
}
