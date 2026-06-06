// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//


namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type
{
    internal sealed class GLSLV430 : IGLSLType
    {
        private static readonly Dictionary<System.Type, (string, VertexAttribPointerType, int)> Infomatons =
            new() {
                {typeof(bool)    , ("bool"  , VertexAttribPointerType.Byte       , 1    )},

                {typeof(int)     , ("int"   , VertexAttribPointerType.Int        , 1    )},
                {typeof(Vector2i), ("ivec2" , VertexAttribPointerType.Int        , 2    )},
                {typeof(Vector3i), ("ivec3" , VertexAttribPointerType.Int        , 3    )},
                {typeof(Vector4i), ("ivec4" , VertexAttribPointerType.Int        , 4    )},

                {typeof(uint)    , ("uint"  , VertexAttribPointerType.UnsignedInt, 1    )},
                // {typeof(Vector2u), ("uvec2" , VertexAttribPointerType.UnsignedInt, 2    )},
                // {typeof(Vector3u), ("uvec3" , VertexAttribPointerType.UnsignedInt, 3    )},
                // {typeof(Vector4u), ("uvec4" , VertexAttribPointerType.UnsignedInt, 4    )},

                {typeof(float)   , ("float" , VertexAttribPointerType.Float      , 1    )},
                {typeof(Vector2) , ("vec2"  , VertexAttribPointerType.Float      , 2    )},
                {typeof(Vector3) , ("vec3"  , VertexAttribPointerType.Float      , 3    )},
                {typeof(Vector4) , ("vec4"  , VertexAttribPointerType.Float      , 4    )},
                {typeof(Matrix2) , ("mat2"  , VertexAttribPointerType.Float      , 2 * 2)},
                {typeof(Matrix3) , ("mat3"  , VertexAttribPointerType.Float      , 3 * 3)},
                {typeof(Matrix4) , ("mat4"  , VertexAttribPointerType.Float      , 4 * 4)},

                {typeof(double)  , ("double", VertexAttribPointerType.Double     , 1    )},
                {typeof(Vector2d), ("dvec2" , VertexAttribPointerType.Double     , 2    )},
                {typeof(Vector3d), ("dvec3" , VertexAttribPointerType.Double     , 3    )},
                {typeof(Vector4d), ("dvec4" , VertexAttribPointerType.Double     , 4    )},
                // {typeof(Matrix2d), ("dmat2" , VertexAttribPointerType.Double     , 2 * 2)},
                // {typeof(Matrix3d), ("dmat3" , VertexAttribPointerType.Double     , 3 * 3)},
                // {typeof(Matrix4d), ("dmat4" , VertexAttribPointerType.Double     , 4 * 4)},
            };

        public string Name { get; private init; }
        public VertexAttribPointerType Type { get; private init; }
        public int LengthOfType { get; private init; }

        public GLSLV430(System.Type type)
        {
            // NOTE: GetTryValueが遅い場合、分岐を無くしてもいいかもしれない。
            if (!Infomatons.TryGetValue(type, out var info))
            {
                throw new KeyNotFoundException("指定された型はGLSL組み込み型として登録されていません。");
            }

            Name = info.Item1;
            Type = info.Item2;
            LengthOfType = info.Item3;
        }

        public IGLSLType TypeOf(System.Type type)
        {
            return new GLSLV430(type);
        }
    }
}
