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
        public EVP1 SkinningEnvelopes { get; private set; }
        public DRW1 DrawData { get; private set; }
        public JNT1 JointData { get; private set; }
        public SHP1 ShapeData { get; private set; }

        public BDL()
        {
            SceneTreeData = new INF1();
            VerTexData = new VTX1();
            SkinningEnvelopes = new EVP1();
            DrawData = new DRW1();
            JointData = new JNT1();
            ShapeData = new SHP1();
        }

        public void Read(FileStream fs)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                SceneTreeData.Read(br);
                VerTexData.Read(br,SceneTreeData.VertexCount);
                SkinningEnvelopes.Read(br);
                DrawData.Read(br);
                JointData.Read(br);
                ShapeData.Read(br);
            }
        }




    }
}
