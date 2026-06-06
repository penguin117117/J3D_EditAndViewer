// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Command;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;

namespace J3DEditorAndViewer.UI.Renderer.Library.GX
{
    internal class GXElementBufferObject : IVBOCommonManager
    {
        private bool _disposed;

        private static readonly BufferTarget gLBufTarget = BufferTarget.ArrayBuffer;
        private int elementObjectsGLBufId;
        private int offsetObjectsGLBufId;

        public DrawArray PrimitiveDrawCommand { get; private set; }
        public GXElement[] ElementObjects { get; private set; }
        public GXElement OffsetObjects { get; private set; }

        private static int vAOStride = Marshal.SizeOf<GXElement>();

        public GXElementBufferObject(Primitive primitive, GXElement offset)
        {
            _disposed = false;

            ElementObjects = primitive.Elements;
            PrimitiveDrawCommand = new(GXPrimitiveConverter.ToGLPrimitive(primitive.Type), 0, ElementObjects.Length);

            elementObjectsGLBufId = GL.GenBuffer();

            GL.BindBuffer(gLBufTarget, elementObjectsGLBufId);
            GL.BufferData(gLBufTarget, Marshal.SizeOf<GXElement>() * ElementObjects.Length, ElementObjects, BufferUsageHint.StaticDraw);

            offsetObjectsGLBufId = GL.GenBuffer();
            GL.BindBuffer(gLBufTarget, offsetObjectsGLBufId);
            GL.BufferData(gLBufTarget, Marshal.SizeOf<GXElement>() * 1, ref OffsetObjects, BufferUsageHint.StaticDraw);

            GL.BindBuffer(gLBufTarget, 0);
        }

        ~GXElementBufferObject()
        {
            Debug.Assert(_disposed, "OpenTKのリソースを利用したコードではデリータが呼ばれる前にDispose()を呼ぶ必要があります。");
        }


        private int WDVE_PositionMatrix(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(int));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                vAOStride, GXVertexDataOffset.PositionMatrix);

            GL.EnableVertexAttribArray(index + 0);


            builder.AppendLine($"layout(location = {index + 0}) in {glslType.Name} VE_{nameof(GXVertexDataOffset.PositionMatrix)};");

            return index + 1;
        }
        private int WDVO_PositionMatrix(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(int));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                0, GXVertexDataOffset.PositionMatrix);

            GL.EnableVertexAttribArray(index + 0);

            builder.AppendLine($"layout(location = {index + 0}) in {glslType.Name} VO_{nameof(GXVertexDataOffset.PositionMatrix)};");

            return index + 1;
        }

        private int WDVE_TexMat(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                vAOStride, GXVertexDataOffset.TexMat0t3);
            GL.VertexAttribPointer(index + 1, glslType.LengthOfType, glslType.Type, false,
                vAOStride, GXVertexDataOffset.TexMat4t7);

            GL.EnableVertexAttribArray(index + 0);
            GL.EnableVertexAttribArray(index + 1);

            builder.AppendLine($"layout(location = {index + 0}) in {glslType.Name} VE_{nameof(GXVertexDataOffset.TexMat0t3)};");
            builder.AppendLine($"layout(location = {index + 1}) in {glslType.Name} VE_{nameof(GXVertexDataOffset.TexMat4t7)};");

            return index + 2;
        }
        private int WDVO_TexMat(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                0, GXVertexDataOffset.TexMat0t3);
            GL.VertexAttribPointer(index + 1, glslType.LengthOfType, glslType.Type, false,
                0, GXVertexDataOffset.TexMat4t7);

            GL.EnableVertexAttribArray(index + 0);
            GL.EnableVertexAttribArray(index + 1);

            builder.AppendLine($"layout(location = {index + 0}) in {glslType.Name} VO_{nameof(GXVertexDataOffset.TexMat0t3)};");
            builder.AppendLine($"layout(location = {index + 1}) in {glslType.Name} VO_{nameof(GXVertexDataOffset.TexMat4t7)};");

            return index + 2;
        }

        private int WDVE_Pos_NBO_Color0t1(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                vAOStride, GXVertexDataOffset.Pos_NBT_Color0t1);

            GL.EnableVertexAttribArray(index + 0);

            builder.AppendLine($"layout(location = {index + 0}) in  {glslType.Name}  VE_{nameof(GXVertexDataOffset.Pos_NBT_Color0t1)};");

            return index + 1;
        }
        private int WDVO_Pos_NBO_Color0t1(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                0, GXVertexDataOffset.Pos_NBT_Color0t1);

            GL.EnableVertexAttribArray(index + 0);

            builder.AppendLine($"layout(location = {index + 0}) in  {glslType.Name} VO_{nameof(GXVertexDataOffset.Pos_NBT_Color0t1)};");

            return index + 1;
        }

        private int WDVE_TexCoord(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                vAOStride, GXVertexDataOffset.TexCoord0t3);
            GL.VertexAttribPointer(index + 1, glslType.LengthOfType, glslType.Type, false,
                vAOStride, GXVertexDataOffset.TexCoord4t7);

            GL.EnableVertexAttribArray(index + 0);
            GL.EnableVertexAttribArray(index + 1);

            builder.AppendLine($"layout(location = {index + 0}) in  {glslType.Name}  VE_{nameof(GXVertexDataOffset.TexCoord0t3)};");
            builder.AppendLine($"layout(location = {index + 1}) in  {glslType.Name} VE_{nameof(GXVertexDataOffset.TexCoord4t7)};");

            return index + 2;
        }
        private int WDVO_TexCoord(StringBuilder builder, IGLSLTypeTraits typeTrait, int index)
        {
            IGLSLType glslType = typeTrait.TypeOf(typeof(Vector4i));

            GL.VertexAttribPointer(index + 0, glslType.LengthOfType, glslType.Type, false,
                0, GXVertexDataOffset.TexCoord0t3);
            GL.VertexAttribPointer(index + 1, glslType.LengthOfType, glslType.Type, false,
                0, GXVertexDataOffset.TexCoord4t7);

            GL.EnableVertexAttribArray(index + 0);
            GL.EnableVertexAttribArray(index + 1);

            builder.AppendLine($"layout(location = {index + 0}) in  {glslType.Name}  VO_{nameof(GXVertexDataOffset.TexCoord0t3)};");
            builder.AppendLine($"layout(location = {index + 1}) in  {glslType.Name}  VO_{nameof(GXVertexDataOffset.TexCoord4t7)};");

            return index + 2;
        }

        [Conditional("DEBUG")]
        private void Debug_AppendLine(StringBuilder builder)
        {
            builder.AppendLine($"// VE_ = Virtual Element");
            builder.AppendLine($"// _XtY = X to Y");
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int beginIndex = 0)
        {
            Debug.Assert(beginIndex == 0, "このインスタンスはVAOの先頭である必要があります。");
            Debug_AppendLine(builder);

            GL.BindBuffer(gLBufTarget, elementObjectsGLBufId);
            GL.VertexBindingDivisor(elementObjectsGLBufId, 0);

            beginIndex = WDVE_PositionMatrix(builder, typeTrait, beginIndex);
            beginIndex = WDVE_TexMat(builder, typeTrait, beginIndex);
            beginIndex = WDVE_Pos_NBO_Color0t1(builder, typeTrait, beginIndex);
            beginIndex = WDVE_TexCoord(builder, typeTrait, beginIndex);

            GL.BindBuffer(gLBufTarget, offsetObjectsGLBufId);
            GL.VertexBindingDivisor(offsetObjectsGLBufId, 0);
            beginIndex = WDVO_PositionMatrix(builder, typeTrait, beginIndex);
            beginIndex = WDVO_TexMat(builder, typeTrait, beginIndex);
            beginIndex = WDVO_Pos_NBO_Color0t1(builder, typeTrait, beginIndex);
            beginIndex = WDVO_TexCoord(builder, typeTrait, beginIndex);

            return beginIndex;
        }

        public void Use()
        {
            GL.BindBuffer(gLBufTarget, elementObjectsGLBufId);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            GL.DeleteBuffer(elementObjectsGLBufId);
        }
    }
}
