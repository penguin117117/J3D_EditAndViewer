using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using J3DEditorAndViewer.Model.GX;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.ViewModel.GX
{
    /// <summary>
    /// GLSLでのVBOを使用してGXでのEBOを実装しています。
    /// </summary>
    internal class VirticesBufferObjectViewModel : IVBOViewModel, IDisposable
    {
        private bool _isDisposed;
        public const BufferTarget Target = BufferTarget.ShaderStorageBuffer;
        private readonly int[] _bufferHandle = [0, 0];
        public int VirtexBufferHandle => _bufferHandle[0];
        public int MatrixVirtexBufferHandle => _bufferHandle[1];

        private readonly string _definication = """
// Virtex0
layout (location = 0x0) in vec4 position;
layout (location = 0x1) in vec4 normal;
layout (location = 0x2) in vec4 tangent;
layout (location = 0x3) in vec4 binormal;
layout (location = 0x4) in vec4 color0;
layout (location = 0x5) in vec4 color1;
layout (location = 0x6, conponent = 0) in vec2 texture_coordinate_0;
layout (location = 0x6, conponent = 2) in vec2 texture_coordinate_1;
layout (location = 0x7, conponent = 0) in vec2 texture_coordinate_2;
layout (location = 0x7, conponent = 2) in vec2 texture_coordinate_3;
layout (location = 0x8, conponent = 0) in vec2 texture_coordinate_4;
layout (location = 0x8, conponent = 2) in vec2 texture_coordinate_5;
layout (location = 0x9, conponent = 0) in vec2 texture_coordinate_6;
layout (location = 0x9, conponent = 2) in vec2 texture_coordinate_7;

// Virtex1
layout (location = 0xa, component = 0) in int element_texture_matrix0;
layout (location = 0xa, component = 1) in int element_texture_matrix1;
layout (location = 0xa, component = 2) in int element_texture_matrix2;
layout (location = 0xa, component = 3) in int element_texture_matrix3;
layout (location = 0xb, component = 0) in int element_texture_matrix4;
layout (location = 0xb, component = 1) in int element_texture_matrix5;
layout (location = 0xb, component = 2) in int element_texture_matrix6;
layout (location = 0xb, component = 3) in int element_texture_matrix7;
layout (location = 0xc) in int element_position_matrix;
""";

        public string Definication => _definication;


        // TODO: ここでGXElementから頂点情報を最適化をする。
        public VirticesBufferObjectViewModel(GXElement[] gXVerticesElement)
        {
            GL.GenBuffers(_bufferHandle.Length, _bufferHandle);
        }

        public VirtexBufferObject[] VirtexBuffer
        {
            set
            {
                GL.BindBuffer(Target, VirtexBufferHandle);
                GL.BufferData(Target, Marshal.SizeOf<VirtexBufferObject>() * value.Length, value, BufferUsageHint.StaticDraw);
                GL.BindBuffer(Target, 0);
            }
        }
        public VirtexMatrixBufferObject[] MatrixVirtexBuffer
        {
            set
            {
                GL.BindBuffer(Target, MatrixVirtexBufferHandle);
                GL.BufferData(Target, Marshal.SizeOf<VirtexMatrixBufferObject>() * value.Length, value, BufferUsageHint.StaticDraw);
                GL.BindBuffer(Target, 0);
            }
        }


        ~VirticesBufferObjectViewModel()
        {
            if (!_isDisposed) throw new Exception("シェーダオブジェクトは手動でDispose()を呼び出す必要があります。");
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            GL.DeleteBuffers(_bufferHandle.Length, _bufferHandle);
            _isDisposed = true;
        }

        public void BindToVAO()
        {
            const int virticesLocation = 0xa;
            const int matricesLocation = 0xd;
            int location = 0;
            for (; location < virticesLocation; location++)
            {
                GL.VertexAttribPointer(location, 4, VertexAttribPointerType.Float, false, Marshal.SizeOf<VirtexBufferObject>() * 1, location * 4);
                GL.EnableVertexAttribArray(location);
            }
            for (; location < matricesLocation; location++)
            {

                GL.VertexAttribPointer(location, 4, VertexAttribPointerType.Int, false, Marshal.SizeOf<VirtexMatrixBufferObject>() * 0, location * 4);
                GL.EnableVertexAttribArray(location);
            }
        }
    }
}
