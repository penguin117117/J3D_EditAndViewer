using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace J3DEditorAndViewer.ViewModel.GX
{
    /// <summary>
    /// GLSLでのUBOを使用してGXでのVAOを実装しています。
    /// </summary>
    internal class ShaderStorageBufferObjectViewModel : ISSBOViewModel, IDisposable
    {
        private bool _isDisposed;
        public const BufferTarget Target = BufferTarget.ShaderStorageBuffer;

        private readonly int[] _bufferHandle;
        public int FloatBufferHandle => _bufferHandle[0];
        public int IntBufferHandle => _bufferHandle[1];


        private readonly string _definication = """
layout(std430, binding = 0) readonly buffer ssbo_vertex_float {
  float value[];
} vertex_float;

layout(std430, binding = 1) readonly buffer ssbo_vertex_int {
  int value[];
} vertex_int;
""";
        public string Definication => _definication;

        public ShaderStorageBufferObjectViewModel(float[] fvertices, int[] ivertices)
        {
            _bufferHandle = [0, 0];
            GL.GenBuffers(_bufferHandle.Length, _bufferHandle);

            GL.BindBuffer(Target, FloatBufferHandle);
            GL.BufferData(Target, sizeof(float) * fvertices.Length, fvertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(Target, IntBufferHandle);
            GL.BufferData(Target, sizeof(int) * ivertices.Length, ivertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(Target, 0);
        }

        ~ShaderStorageBufferObjectViewModel()
        {
            if (!_isDisposed) throw new Exception("シェーダオブジェクトは手動でDispose()を呼び出す必要があります。");
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                GL.DeleteBuffers(_bufferHandle.Length, _bufferHandle);
                _isDisposed = true;
            }
        }

        public void BindToShader()
        {
            const int locationCount = 5;
            for (int location = 0; location < locationCount; location++)
            {
                GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, FloatBufferHandle);
                GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 1, IntBufferHandle);
            }
        }
    }
}
