// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.EBO
{
    internal class TriangleEBOManager : IEBOManager
    {
        private bool disposed = false;
        private bool isModified = false;

        public uint[] data;
        public uint[] Data
        {
            get { return data; }
            set
            {
                isModified |= true;
                data = value;
            }
        }

        readonly private int BufferIndex;

        private IAutoObjectBinder CreateObjectBinder()
        {
            return new AutoElementArrayBufferBinder(BufferIndex);
        }

        public TriangleEBOManager(uint[] FaceIndexes)
        {
            data = FaceIndexes;
            isModified = true;

            // 頂点インデックスバッファを生成
            BufferIndex = GL.GenBuffer();

            UpdateBuffer();
        }

        ~TriangleEBOManager()
        {
            if (!disposed)
            {
                throw new Exception("Dispose が呼ばれていません。");
            }
        }

        public void Dispose()
        {
            if (disposed) { return; }
            GL.DeleteBuffer(BufferIndex);
        }

        public void Use()
        {
            using var aob = CreateObjectBinder();
            UpdateBuffer(aob);
            GL.DrawElements(PrimitiveType.Triangles, Data.Length, DrawElementsType.UnsignedInt, 0);
        }
        private void UpdateBuffer(IAutoObjectBinder aob)
        {
            if (!isModified) { return; }
            isModified = false;
            GL.BufferData(BufferTarget.ElementArrayBuffer, Data.Length * sizeof(uint), Data, BufferUsageHint.StaticDraw);
        }
        private void UpdateBuffer()
        {
            using var aob = CreateObjectBinder();
            UpdateBuffer(aob);
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int location = 0)
        {
            // シェーダコードに関わらないため実装しません。
            throw new NotImplementedException();
        }
    }
}
