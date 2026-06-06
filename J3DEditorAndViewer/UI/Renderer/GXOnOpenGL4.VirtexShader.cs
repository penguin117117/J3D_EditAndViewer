using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.Model.GX;
using OpenTK.Graphics.OpenGL4;

namespace J3DEditorAndViewer.UI.Renderer
{
    internal partial class GXOnOpenGL4
    {
        private string vartShaderSrc = @"
#version 430

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

out vec4 color;

void main() {
    gl_Position = position;
    color = color0;
}
";
        private int vao = 0;
        private void InitVAO()
        {
            vao = GL.GenVertexArray();
        }
        private void disposeVAO()
        {
            GL.DeleteVertexArray(vao);
        }
        private void VBOBinding()
        {
            GL.BindVertexArray(vao);

            const int virticesLocation = 0xa;
            const int matricesLocation = 0xd;
            int location = 0;
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo[0]);
            for (; location < virticesLocation; location++)
            {
                GL.VertexAttribPointer(location, 4, VertexAttribPointerType.Float, false, Marshal.SizeOf<VirtexBufferObject>() * 1, location * 4);
                GL.EnableVertexAttribArray(location);
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo[1]);
            for (; location < matricesLocation; location++)
            {

                GL.VertexAttribPointer(location, 4, VertexAttribPointerType.Int, false, Marshal.SizeOf<VirtexMatrixBufferObject>() * 0, location * 4);
                GL.EnableVertexAttribArray(location);
            }
            GL.BindVertexArray(0);
        }

        private int[] vbo = [0, 0];
        private void InitVBO()
        {
            GL.GenBuffers(vbo.Length, vbo);
        }
        private void disposeVBO()
        {
            GL.DeleteBuffers(vbo.Length, vbo);
        }
        private void VirtexBinding(VirtexBufferObject[] virtices)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf<VirtexBufferObject>() * virtices.Length, virtices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        private void VirtexMatrixBinding(VirtexMatrixBufferObject[] virtexMatrix)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf<VirtexBufferObject>(), virtexMatrix, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
