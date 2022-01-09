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

        public BDL()
        {
            SceneTreeData = null;
        }

        public void Read(FileStream fs)
        {
            SceneTreeData = new INF1();

            using (BinaryReader br = new BinaryReader(fs))
            {
                SceneTreeData.Read(br);
            }
        }




    }
}
