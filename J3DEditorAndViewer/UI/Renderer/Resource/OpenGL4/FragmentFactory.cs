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
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.UBO;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4
{
    internal class FragmentFactory : IFragmentFactory
    {
        private bool disposed;
        private string SourceCode;

        private IUBOCommonManager[] UniformManagers;
        private IGLSLTypeTraits GLSLTypeTrait;
        public ShaderType ShaderType { get { return ShaderType.FragmentShader; } }

        public FragmentFactory(IUBOCommonManager[] uniformManagers, IGLSLTypeTraits typeTrait, string sourceCode)
        {
            UniformManagers = uniformManagers;
            GLSLTypeTrait = typeTrait;
            SourceCode = sourceCode + "\n";
        }
        public FragmentFactory(IGLSLTypeTraits typeTrait, string sourceCode)
        {
            UniformManagers = new IUBOCommonManager[] { };
            GLSLTypeTrait = typeTrait;
            SourceCode = sourceCode + "\n";
        }

        ~FragmentFactory()
        {
            if (!disposed)
            {
                throw new Exception("Dispose が呼ばれていません。");
            }
        }

        public int WriteDefinicator(StringBuilder builder, in int beginIndex)
        {
            int currentIndex = beginIndex;

            foreach (var uniform in UniformManagers)
            {
                currentIndex = uniform.WriteDefinicator(builder, GLSLTypeTrait, currentIndex);
            }

            builder.Append(SourceCode);

            return currentIndex;
        }

        public void Dispose()
        {
            if (disposed) { return; }
            disposed = true;
            foreach (var uniform in UniformManagers)
            {
                uniform.Dispose();
            }
        }

        public void Use()
        {
            foreach (var uniform in UniformManagers)
            {
                uniform.Use();
            }
        }

    }
}
