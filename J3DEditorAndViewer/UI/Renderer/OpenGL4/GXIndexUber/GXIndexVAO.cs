// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using GalaxyPlantInCrystal_KEIJI.IO.BinarySystem.Util;
using J3DEditorAndViewer.FileFormat;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using J3DEditorAndViewer.UI.Renderer.Library.GX;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace J3DEditorAndViewer.UI.Renderer.OpenGL4.GXIndexUber
{
    internal class GXIndexVAO : IDisposable
    {
        bool _disposed;

        public enum VertexLocation : int
        {
            E_PositionMatrix = 0,
            E_TexMat0t3 = 1,
            E_TexMat4t7 = 2,
            E_Pos_NBT_Color0t1 = 3,
            E_TexCoord0t3 = 4,
            E_TexCoord4t7 = 5,
            O_PositionMatrix = 6,
            O_TexMat0t3 = 7,
            O_TexMat4t7 = 8,
            O_Pos_NBT_Color0t1 = 9,
            O_TexCoord0t3 = 10,
            O_TexCoord4t7 = 11,
            Part_ModelPosMat0 = 12,
            Part_ModelPosMat1 = 13,
            Part_ModelPosMat2 = 14,
        }

        private readonly int GLVertexArrayID;
        private readonly int VerticesGLBufferID;
        private readonly int PosMatGLBufferID;

        private PrimitiveType GLPrimitiveType;
        private GXElement[] VerticesElements;
        private Matrix4x3[] VerticesMatrix;
        private readonly int VerticesElementsStrideSize = Marshal.SizeOf<GXElement>();

        public GXIndexVAO(Primitive primitive)
        {
            GL.GenBuffers(2, [VerticesGLBufferID, PosMatGLBufferID]);
            GLPrimitiveType = GXPrimitiveConverter.ToGLPrimitive(primitive.Type);
            VerticesElements = primitive.Elements;
            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesGLBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VerticesElementsStrideSize * VerticesElements.Length, VerticesElements, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesGLBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, 0, IntPtr.Zero, BufferUsageHint.DynamicDraw);


            GLVertexArrayID = GL.GenVertexArray();
            GL.BindVertexArray(GLVertexArrayID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesGLBufferID);

            GL.VertexAttribIPointer((int)VertexLocation.E_PositionMatrix, 1, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.PositionMatrix)));
            GL.VertexAttribIPointer((int)VertexLocation.E_TexMat0t3, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.TextureMatrix0)));
            GL.VertexAttribIPointer((int)VertexLocation.E_TexMat4t7, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.TextureMatrix4)));
            GL.VertexAttribIPointer((int)VertexLocation.E_Pos_NBT_Color0t1, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.Position)));
            GL.VertexAttribIPointer((int)VertexLocation.E_TexCoord0t3, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.TextureCoordinate0)));
            GL.VertexAttribIPointer((int)VertexLocation.E_TexCoord4t7, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.TextureCoordinate4)));

            GL.VertexAttribPointer((int)VertexLocation.Part_ModelPosMat0, 1, VertexAttribPointerType.Float, false, VerticesElementsStrideSize, 0);
            GL.VertexAttribPointer((int)VertexLocation.Part_ModelPosMat1, 1, VertexAttribPointerType.Float, false, VerticesElementsStrideSize, 4);
            GL.VertexAttribPointer((int)VertexLocation.Part_ModelPosMat2, 1, VertexAttribPointerType.Float, false, VerticesElementsStrideSize, 8);

            GL.VertexAttribIPointer((int)VertexLocation.O_PositionMatrix, 1, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.PositionMatrix)));
            GL.VertexAttribIPointer((int)VertexLocation.O_TexMat0t3, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.PositionMatrix)));
            GL.VertexAttribIPointer((int)VertexLocation.O_TexMat4t7, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.PositionMatrix)));
            GL.VertexAttribIPointer((int)VertexLocation.O_Pos_NBT_Color0t1, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, Marshal.OffsetOf<GXElement>(nameof(GXElement.Color0)) * 2);
            GL.VertexAttribIPointer((int)VertexLocation.O_TexCoord0t3, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, 0);
            GL.VertexAttribIPointer((int)VertexLocation.O_TexCoord4t7, 4, VertexAttribIntegerType.Int, VerticesElementsStrideSize, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UpdatePosMatrix()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesGLBufferID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, 0, Marshal.SizeOf<Matrix4x3>() * VerticesMatrix.Length, VerticesMatrix);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void DrawInstance()
        {
            GL.BindVertexArray(GLVertexArrayID);
            GL.DrawArraysInstanced(GLPrimitiveType, 0, VerticesElements.Length, VerticesMatrix.Length);
        }

        public void Update()
        {
            UpdatePosMatrix();
        }

        public void Dispose()
        {
            if (_disposed) return;
        }
    }
}
