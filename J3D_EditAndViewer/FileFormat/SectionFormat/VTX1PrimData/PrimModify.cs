using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace J3D_EditAndViewer.FileFormat.SectionFormat.VTX1PrimData
{
    
    public class PrimModify<T>
    {
        

        public PrimModify(T type) 
        {
            
        }



        public void ChangeType<T>(BinaryReader br, T type)
        {
            List<T> a = new List<T>();
            Test(a);
        }

        public List<T> Test<T>(List<T> a) 
        {
            return a;
        }
    }
}
