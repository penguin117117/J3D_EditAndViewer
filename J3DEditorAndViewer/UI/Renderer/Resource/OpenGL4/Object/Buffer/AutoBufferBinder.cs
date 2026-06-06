// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer
{
    /// <summary>
    /// ブロックでのBindBuffer動作を楽に実装するためのクラス。<br/>
    /// クラスでの使用を除き、基本using束縛して使用して下さい。
    /// </summary>
    internal abstract class AutoBufferBinder : IAutoObjectBinder
    {
        internal bool disposed;

        internal readonly BufferTarget BufferTarget;
        internal readonly int Handle;

        public AutoBufferBinder(BufferTarget bufferTarget, int bufferIndex)
        {
            BufferTarget = bufferTarget;
            Handle = bufferIndex;
            GL.BindBuffer(BufferTarget, Handle);
        }
        ~AutoBufferBinder()
        {
            if (!disposed)
            {
                throw new Exception("Dispose が呼ばれていません。");
            }
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            GL.BindBuffer(BufferTarget, 0);
        }

        public IAutoObjectBinder MoveObject() => throw new NotImplementedException();
        //{
        //    if (disposed == true) throw new ObjectDisposedException(GetType().Name);
        //    disposed = true;
        //    return new AutoObjectBinder(BufferTarget, BufferIndex);
        //}
    }
}
