using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem
{
    internal interface INameTable
    {
        short Hash { get; }
        short Offset { get; }
    }
}
