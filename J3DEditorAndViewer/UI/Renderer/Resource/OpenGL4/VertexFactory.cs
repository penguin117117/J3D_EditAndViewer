// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.UBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.VBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Array.VAO;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4
{
    internal class VertexFactory : IVertexFactory
    {
        private bool disposed;
        private string SourceCode;

        private IVAOCommonManager VAOCommonManager;
        private IUBOCommonManager[] UniformManagers;
        private IGLSLTypeTraits GLSLTypeTrait;
        public ShaderType ShaderType { get { return ShaderType.VertexShader; } }

        public VertexFactory(IVAOCommonManager vAOCommonManager, IUBOCommonManager[] uniformManagers, IGLSLTypeTraits typeTrait, string sourceCode)
        {
            GLSLTypeTrait = typeTrait;
            VAOCommonManager = vAOCommonManager;
            UniformManagers = uniformManagers;
            SourceCode = sourceCode + "\n";
        }
        public VertexFactory(IVAOCommonManager vAOCommonManager, IGLSLTypeTraits typeTrait, string sourceCode)
        {
            GLSLTypeTrait = typeTrait;
            VAOCommonManager = vAOCommonManager;
            UniformManagers = new IUBOCommonManager[] { };
            SourceCode = sourceCode + "\n";
        }

        ~VertexFactory()
        {
            if (!disposed)
            {
                throw new Exception("Dispose が呼ばれていません。");
            }
        }

        public int WriteDefinicator(StringBuilder builder)
        {
            VAOCommonManager.WriteDefinicator(builder, GLSLTypeTrait);

            int currentIndexBinding = 0;
            foreach (var uniform in UniformManagers)
            {
                currentIndexBinding = uniform.WriteDefinicator(builder, GLSLTypeTrait, currentIndexBinding);
            }

            builder.Append(SourceCode);

            return currentIndexBinding;
        }

        public void Dispose()
        {
            if (disposed) { return; }
            disposed = true;
            foreach (var uniform in UniformManagers)
            {
                uniform.Dispose();
            }

            VAOCommonManager.Dispose();

        }

        public void Use()
        {
            VAOCommonManager.Use();


            foreach (var uniform in UniformManagers)
            {
                uniform.Use();
            }
        }
    }
}
