using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.ViewModel.GX
{
    internal interface ISSBOViewModel
    {
        public string Definication { get; }
        public void BindToShader();
    }
}
