// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;


namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO
{
    /// <summary>
    /// struct内で定義されたGLSL互換型の配列を扱うクラス。
    /// </summary>
    /// <typeparam name="InT"></typeparam>
    internal class StructVBOManager<InT> : IVBOManager<InT>
        where InT : struct//, IVertexObject
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

        private IAutoObjectBinder CreateObjectBinder()
        {
            return new AutoVertexBufferBinder(BufferIndex);
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int location = 0)
        {
            location = 0;
            using var aob = CreateObjectBinder();

            // var MemberInfo = VertexObjectT.MemberNames[location];
            var MembersInfo = typeof(InT).GetFields();
            foreach (var MemberInfo in MembersInfo)
            {
                IGLSLType GLSLType = typeTrait.TypeOf(MemberInfo.FieldType);

                builder.Append($"layout(location = {location}) in {GLSLType.Name} {MemberInfo.Name};\n");
                GL.VertexAttribPointer(location, GLSLType.LengthOfType, GLSLType.Type, false, SizeInBytes, Marshal.OffsetOf<InT>(MemberInfo.Name));
                GL.EnableVertexAttribArray(location);
                location++;
            }

            // 次の location = X を返す。
            return location;
        }

        public StructVBOManager(InT[] VertexData)
        {
            data = VertexData;
            isModified = true;


            // BufferObjectの確保。このクラスは構造体を利用するため1つのみ。
            BufferIndex = GL.GenBuffer();
            UpdateBuffer();
        }

        ~StructVBOManager()
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
            using var aob = CreateObjectBinder();
            isModified = false;
            GL.BufferData(BufferTarget.ArrayBuffer, LengthInBytes, data, BufferUsageHint.StaticDraw);
        }

        public void Use()
        {
            //using var abb = AutoBinder.Use();
            UpdateBuffer();
        }
    }
}