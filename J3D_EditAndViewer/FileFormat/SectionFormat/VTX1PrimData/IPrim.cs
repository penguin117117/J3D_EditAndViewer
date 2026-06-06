using System.IO;
using OpenTK;

namespace J3DEditorAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    //public interface IPrim<T>:IPrimGet<T>
    //{
    //    void Set(BinaryReader br);
    //}
    public interface IPrim
    {
        Vector3 GetData { get; }

        Vector3 Set(BinaryReader br,byte shift = 0);
    }
}