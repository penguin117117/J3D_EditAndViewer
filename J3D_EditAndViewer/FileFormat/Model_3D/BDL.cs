using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using J3D_EditAndViewer.FileFormat;
using J3D_EditAndViewer.FileFormat.SectionFormat;

namespace J3D_EditAndViewer.FileFormat.Model_3D
{
    public class BDL : IModel_3D
    {
        
        public INF1 SceneTreeData { get; private set; }
        public VTX1 VerTexData { get; private set; }

        public BDL()
        {
            SceneTreeData = new INF1();
            VerTexData = new VTX1();
        }

        public void Read(FileStream fs)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                SceneTreeData.Read(br);
                VerTexData.Read(br,SceneTreeData.VertexCount);
            }
        }




    }
}
