using GalaxyPlantInCrystal_KEIJI.EndianBinaryStream.BinaryReaderSystem;
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.FileFormat.JSystem
{
    /// <summary>
    /// https://wiki.cloudmodding.com/zgcn/JSYSTEM#Common_Conventions_of_some_JSYSTEM_binary_files
    /// </summary>
    public interface IHeader
    {
        long BeginPosition { get; }
        uint SubSystem { get; } // Subsystem Version
        uint SubSystemType { get; } // File Type
        int SizeInBytes { get; } // File Size
        int SectionCount { get; } // Chunk Count
        int SubSystemMarker { get; } // Subversion
    }
}
