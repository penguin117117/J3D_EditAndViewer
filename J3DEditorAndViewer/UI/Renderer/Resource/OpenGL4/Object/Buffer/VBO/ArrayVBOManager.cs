// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO
{
    /// <summary>
    /// Vector4などのGLSL互換型の配列を扱うクラス。
    /// </summary>
    /// <typeparam name="InT"></typeparam>
    internal class ValueVBOManager<InT> : IVBOManager<InT>
        where InT : struct
    {
        private bool disposed = false;
        private bool isModified = false;

        private InT[] data;
        public InT[] Data
        {
            get { return data; }
            set
            {
                isModified |= true;
                data = value;
            }
        }
        public string DefineName { get; private set; }
        /// <summary>
        /// 型Tのバイト数
        /// </summary>
        private readonly int SizeInBytes = Marshal.SizeOf<InT>();
        /// <summary>
        /// 配列の要素数
        /// </summary>
        private int Length
        {
            get { return Data.Length; }
        }
        /// <summary>
        /// 配列のバイト数
        /// </summary>
        private int LengthInBytes
        {
            get { return Length * SizeInBytes; }
        }

        readonly private int BufferIndex;

        private int getStrideSize()
        {
            if (data.Length <= 1) return 0;
            return SizeInBytes;
        }

        private IAutoObjectBinder CreateObjectBinder()
        {
            return new AutoVertexBufferBinder(BufferIndex);
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int location = 0)
        {
            location = 0;
            using var aob = CreateObjectBinder();

            var type = typeof(InT);
            IGLSLType GLSLType = typeTrait.TypeOf(type);

            builder.Append($"layout(location = {location}) in {GLSLType.Name} {DefineName};\n");
            GL.VertexAttribPointer(location, GLSLType.LengthOfType, GLSLType.Type, false, getStrideSize(), 0);
            GL.EnableVertexAttribArray(location);
            location++;

            // 次の location = X を返す。
            return location;
        }

        public ValueVBOManager(string defineName, InT[] VertexData)
        {
            DefineName = defineName;
            data = VertexData;
            isModified = true;

            // BufferObjectの確保。このクラスは構造体を利用するため1つのみ。
            BufferIndex = GL.GenBuffer();
            UpdateBuffer();
        }

        ~ValueVBOManager()
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
            GL.DeleteBuffer(BufferIndex);
        }

        public void UpdateBuffer()
        {
            if (!isModified) { return; }
            isModified = false;
            using var aob = CreateObjectBinder();
            GL.BufferData(BufferTarget.ArrayBuffer, LengthInBytes, data, BufferUsageHint.StaticDraw);
        }

        public void Use()
        {
            //using var abb = AutoBinder.Use();
            UpdateBuffer();
        }
    }
}
