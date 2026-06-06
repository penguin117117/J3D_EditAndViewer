using System.IO;
using J3DEditorAndViewer.FileFormat.SectionFormat;

namespace J3DEditorAndViewer.FileFormat.Model_3D
{
    public interface IModel_3D
    {
        //byte HierarchyDepth { get; }
        INF1 SceneTreeData { get; }
        VTX1 VerTexData { get; }
        void Read(FileStream fs);
    }
}