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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.SSBO
{
    internal class ArraySSBOManager<ShaderStrageT> : ISSBOManager<ShaderStrageT[]>
        where ShaderStrageT : struct
    {
        private bool disposed = false;
        private bool isModified = false;
        private bool isDummy = false;

        private ShaderStrageT[] data;
        public ShaderStrageT[] Data
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
        private readonly int SizeInBytes = Marshal.SizeOf<ShaderStrageT>();
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

        public readonly BufferUsageHint BufferUsageHint;

        private IAutoObjectBinder CreateObjectBinder()
        {
            return new AutoShaderStrageBufferBinder(BufferIndex);
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int location = 0)
        {
            var membersInfo = typeof(ShaderStrageT).GetFields();
            builder.Append($"layout(std140, binding = {location}) uniform {DefineName} {{\n");
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, location, BufferIndex);
            foreach (int I in Enumerable.Range(0, membersInfo.Length))
            {
                var memberInfo = membersInfo[I];
                IGLSLType typeGLSL = typeTrait.TypeOf(memberInfo.FieldType);

                builder.Append($"  {typeGLSL.Name}[] {memberInfo.Name};\n");
            }
            builder.Append("};\n");
            location++;

            return location;
        }


        public ArraySSBOManager(string defineName, in ShaderStrageT[] shaderStrage, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw)
        {
            data = shaderStrage;
            BufferUsageHint = bufferUsageHint;

            // BufferObjectの確保。
            BufferIndex = GL.GenBuffer();

            using var aob = CreateObjectBinder();
            if (bufferUsageHint == BufferUsageHint.DynamicDraw)
            {
                GL.BufferData(BufferTarget.ShaderStorageBuffer, LengthInBytes, 0, BufferUsageHint);
            }
            else
            {
                GL.BufferData(BufferTarget.ShaderStorageBuffer, LengthInBytes, data, BufferUsageHint);
            }

            DefineName = defineName;
        }
        ~ArraySSBOManager()
        {
            Debug.Assert(!disposed, "Dispose が呼ばれていません。");
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            GL.DeleteBuffer(BufferIndex);
        }

        private void UpdateBuffer(IAutoObjectBinder aob)
        {
            if (BufferUsageHint != BufferUsageHint.DynamicDraw) return;
            GL.BufferSubData(BufferTarget.ShaderStorageBuffer, 0, SizeInBytes, data);
        }
        private void UpdateBuffer()
        {
            using var aob = CreateObjectBinder();
            UpdateBuffer(aob);
        }

        public void Use()
        {
            UpdateBuffer();
        }
    }
}
