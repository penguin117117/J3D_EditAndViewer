// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace J3DEditorAndViewer.UI.Renderer.Library
{
    internal class VBOModelMatrix : IVBOCommonManager
    {
        bool _disposed;

        private static readonly BufferTarget gLBufTarget = BufferTarget.ArrayBuffer;
        private int matrixObjectsGLBufId;

        public Matrix4[] Matrix { get; private set; }

        public VBOModelMatrix(Matrix4[] matrix)
        {
            _disposed = false;

            if (matrix.Length == 0) throw new NullReferenceException("長さは0より大きい必要があります。");

            Matrix = matrix;

            matrixObjectsGLBufId = GL.GenBuffer();

            GL.BindBuffer(gLBufTarget, matrixObjectsGLBufId);
            GL.BufferData(gLBufTarget, 0, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(gLBufTarget, 0);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
        }

        public void Update()
        {
            // UBOでの更新ではAPI呼び出しが多くなる。
            // その回避策としてVBO+VertexBindingDivisor+DrawArraysInstancedで対応する。
            // また、コピーのコストも少なくなるはずです。

            GL.BindBuffer(gLBufTarget, matrixObjectsGLBufId);
            GL.BufferSubData(gLBufTarget, 0, Marshal.SizeOf<Matrix4>() * Matrix.Length, Matrix);
            GL.BindBuffer(gLBufTarget, 0);
        }

        public void Use()
        {
            GL.BindBuffer(gLBufTarget, matrixObjectsGLBufId);
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int beginIndex = 0)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.BindBuffer(gLBufTarget, matrixObjectsGLBufId);
            GL.VertexBindingDivisor(matrixObjectsGLBufId, Marshal.SizeOf<Matrix4>() / Marshal.SizeOf<Vector4>());

            GL.VertexAttribPointer(beginIndex + 0, glslType.LengthOfType, glslType.Type, false,
                0, glslType.LengthOfType * 0);
            GL.VertexAttribPointer(beginIndex + 1, glslType.LengthOfType, glslType.Type, false,
                0, glslType.LengthOfType * 1);
            GL.VertexAttribPointer(beginIndex + 2, glslType.LengthOfType, glslType.Type, false,
                0, glslType.LengthOfType * 2);
            GL.VertexAttribPointer(beginIndex + 3, glslType.LengthOfType, glslType.Type, false,
                0, glslType.LengthOfType * 3);

            GL.EnableVertexAttribArray(beginIndex + 0);
            GL.EnableVertexAttribArray(beginIndex + 1);
            GL.EnableVertexAttribArray(beginIndex + 2);
            GL.EnableVertexAttribArray(beginIndex + 3);

            GL.BindBuffer(gLBufTarget, 0);

            return beginIndex + 4;
        }
    }
}
