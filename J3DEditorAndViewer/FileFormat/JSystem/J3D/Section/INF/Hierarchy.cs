using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.INF
{
    public class Hierarchy
    {
        public enum NodeType : ushort
        {
            FinishNode = 0x0000,
            NewNode = 0x0001,
            EndNode = 0x0002,
            Joint = 0x0010,
            Material = 0x0011,
            Geometry = 0x0012
        }

        public NodeType Type { get; private set; }
        public short ManageID { get; private set; }

        public Hierarchy(EndianBinaryReaderBase br)
        {
            Type = (NodeType)br.ReadInt16();
            ManageID = br.ReadInt16();
        }
        public int IsStepNest()
        {
            switch (Type)
            {
                case NodeType.NewNode: return 1;
                case NodeType.EndNode: return -1;
                default: return 0;
            }
        }
        public bool IsFinishd()
        {
            return Type == NodeType.FinishNode;
        }
    }
}
