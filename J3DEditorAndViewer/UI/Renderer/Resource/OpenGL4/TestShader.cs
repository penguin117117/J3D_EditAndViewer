// OpenTK
using J3DEditorAndViewer.FileFormat.SectionFormat;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4
{
    internal class TestShader : IShader
    {
        private bool disposed = false;

        private int Handle;
        private readonly string Version = "#version 430 core\n";

        private IVertexFactory VertexFactory;
        private IFragmentFactory FragmentFactory;

        private (int, int) ShaderCompilerVertex(IVertexFactory res)
        {
            int shaderID = GL.CreateShader(ShaderType.VertexShader);

            StringBuilder builder = new(Version);
            int bindingID = res.WriteDefinicator(builder);

            Debug.WriteLine($"Shader Source:\n{builder.ToString()}");
            GL.ShaderSource(shaderID, builder.ToString());
            GL.CompileShader(shaderID);
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                Debug.Assert(success != 0, GL.GetShaderInfoLog(shaderID));
            }

            return (shaderID, bindingID);
        }
        private int ShaderCompilerFragment(IFragmentFactory res, int bindingID)
        {
            int shaderID = GL.CreateShader(ShaderType.FragmentShader);

            StringBuilder builder = new(Version);
            res.WriteDefinicator(builder, bindingID);

            Debug.WriteLine($"Shader Source:\n{builder.ToString()}");
            GL.ShaderSource(shaderID, builder.ToString());
            GL.CompileShader(shaderID);
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                Debug.Assert(success != 0, GL.GetShaderInfoLog(shaderID));
            }

            return shaderID;
        }

        private static int ShaderLinker(int vertex, int fragment)
        {
            int handle = GL.CreateProgram();

            GL.AttachShader(handle, vertex);
            GL.AttachShader(handle, fragment);

            GL.LinkProgram(handle);

            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                Debug.Assert(success != 0, GL.GetProgramInfoLog(handle));
            }


            GL.DetachShader(handle, vertex);
            GL.DetachShader(handle, fragment);

            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);

            return handle;
        }

        private int ShaderBuilder()
        {
            (int vertexShader, int bindingID) = ShaderCompilerVertex(VertexFactory);
            int fragmentShader = ShaderCompilerFragment(FragmentFactory, bindingID);

            return ShaderLinker(vertexShader, fragmentShader);
        }

        public TestShader(IVertexFactory vertexFactory, IFragmentFactory fragmentFactory)
        {
            VertexFactory = vertexFactory;
            FragmentFactory = fragmentFactory;
            Handle = ShaderBuilder();
        }

        ~TestShader()
        {
            if (!disposed)
            {
                throw new Exception("Dispose が呼ばれていません。");
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                GL.DeleteProgram(Handle);
            }
            disposed = true;

            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }
    }
}
