using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem
{
    public interface ISectionHeader
    {
        long BeginPosition { get; }
        uint Type { get; }
        int SizeInBytes { get; }
    }
}
