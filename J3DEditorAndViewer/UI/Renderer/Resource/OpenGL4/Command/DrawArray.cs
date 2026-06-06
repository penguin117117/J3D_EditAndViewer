// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Command
{
    internal class DrawArray
    {
        public PrimitiveType PrimitiveType { get; private init; }
        public int Begin { get; private init; }
        public int Size { get; private init; }

        public DrawArray(PrimitiveType primitiveType, int begin, int size)
        {
            PrimitiveType = primitiveType;
            Begin = begin;
            Size = size;
        }

        public void Draw()
        {
            GL.DrawArrays(PrimitiveType, Begin, Size);
        }
        public void Draw(int instanceCount)
        {
            GL.DrawArraysInstanced(PrimitiveType, Begin, Size, instanceCount);
        }
    }
}
