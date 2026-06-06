// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.SHP;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Section.VTX;
using J3DEditorAndViewer.FileFormat.JSystem.J3D.Types;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.SSBO;
using System.Diagnostics;

namespace J3DEditorAndViewer.UI.Renderer.Library.GX
{
    internal class GXVertexBufferObject : ISSBOCommonManager
    {
        private bool _disposed;

        private static string GLSLInitVertex = @$"
mat4 PosMat;
mat2 TexMat[8];
vec4 Pos;
vec3 NBT[3];
ivec4 Color[2];
vec4 TexCoord[8];

vec3 ArrayToVec3(int Offset, int Index) {{
    Index = Index * 3;
    return vecr(
        FloatVertex.Data[Offset + Index + 0],
        FloatVertex.Data[Offset + Index + 1],
        FloatVertex.Data[Offset + Index + 2]);
}}
vec4 ArrayToVec4(int Offset, int Index) {{
    Index = Index * 4;
    return vec4(
        FloatVertex.Data[Offset + Index + 0],
        FloatVertex.Data[Offset + Index + 1],
        FloatVertex.Data[Offset + Index + 2],
        FloatVertex.Data[Offset + Index + 3]);
}}
ivec4 ArrayToIVec4(int Offset, int Index) {{
    Index = Index * 4;
    return ivec4(
        IntVertex.Data[Offset + Index + 0],
        IntVertex.Data[Offset + Index + 1],
        IntVertex.Data[Offset + Index + 2],
        IntVertex.Data[Offset + Index + 3]);
}}
mat2 ArrayToMat2(int Offset, int Index) {{
    Index = Index * (2 * 2);
    return mat2(
        FloatVertex.Data[Offset + Index + 0],
        FloatVertex.Data[Offset + Index + 1],
        FloatVertex.Data[Offset + Index + 2],
        FloatVertex.Data[Offset + Index + 3]);
}}
mat3 ArrayToMat3(int Offset, int Index) {{
    Index = Index * (3 * 3);
    return mat3(
        FloatVertex.Data[Offset + Index + 0],
        FloatVertex.Data[Offset + Index + 1],
        FloatVertex.Data[Offset + Index + 2],
        FloatVertex.Data[Offset + Index + 3],
        FloatVertex.Data[Offset + Index + 4],
        FloatVertex.Data[Offset + Index + 5],
        FloatVertex.Data[Offset + Index + 6],
        FloatVertex.Data[Offset + Index + 7],
        FloatVertex.Data[Offset + Index + 8]);
}}

void InitTexMat() {{
    ivec4 Offset;
    ivec4 Index;

    Offset = VO_{nameof(GXVertexDataOffset.TexMat0t3)};
    Index = VE_{nameof(GXVertexDataOffset.TexMat0t3)};
    TexMat[0] = ArrayToMat2(Offset.x, Index.x);
    TexMat[1] = ArrayToMat2(Offset.y, Index.y);
    TexMat[2] = ArrayToMat2(Offset.z, Index.z);
    TexMat[3] = ArrayToMat2(Offset.w, Index.w);

    Offset = VO_{nameof(GXVertexDataOffset.TexMat4t7)};
    Index = VE_{nameof(GXVertexDataOffset.TexMat4t7)};
    TexMat[4] = ArrayToMat2(Offset.x, Index.x);
    TexMat[5] = ArrayToMat2(Offset.y, Index.y);
    TexMat[6] = ArrayToMat2(Offset.z, Index.z);
    TexMat[7] = ArrayToMat2(Offset.w, Index.w);
}};
void InitPos_NBT_Color(){{
    ivec4 Offset;
    ivec4 Index;

    Offset = VO_{nameof(GXVertexDataOffset.Pos_NBT_Color0t1)};
    Index = VE_{nameof(GXVertexDataOffset.Pos_NBT_Color0t1)};
    Pos = ArrayToVec4(Offset.x, Index.x);
    mat3 MatNBT = ArrayToMat3(Offset.y, Index.y);
    NBT[0] = MatNBT[0];
    NBT[1] = MatNBT[1];
    NBT[2] = MatNBT[2];
    Color[0] = ArrayToIVec4(Offset.z, Index.z);
    Color[1] = ArrayToIVec4(Offset.w, Index.w);
}}
void InitTexCoord(){{
    ivec4 Offset;
    ivec4 Index;

    Offset = VO_{nameof(GXVertexDataOffset.TexCoord0t3)};
    Index = VE_{nameof(GXVertexDataOffset.TexCoord0t3)};
    TexCoord[0] = ArrayToIVec4(Offset.x, Index.x);
    TexCoord[1] = ArrayToIVec4(Offset.y, Index.y);
    TexCoord[2] = ArrayToIVec4(Offset.z, Index.z);
    TexCoord[3] = ArrayToIVec4(Offset.w, Index.w);

    Offset = VO_{nameof(GXVertexDataOffset.TexCoord4t7)};
    Index = VE_{nameof(GXVertexDataOffset.TexCoord4t7)};
    TexCoord[4] = ArrayToIVec4(Offset.x, Index.x);
    TexCoord[5] = ArrayToIVec4(Offset.y, Index.y);
    TexCoord[6] = ArrayToIVec4(Offset.z, Index.z);
    TexCoord[7] = ArrayToIVec4(Offset.w, Index.w);
}}

void InitVertex() {{
    InitTexMat();
    InitPos_NBT_Color();
    InitTexCoord();
}}";

        private static BufferTarget GLBufTarget;
        private int GLFloatBufId;
        private int GLIntBufId;
        private int GLFloatBindId;
        private int GLIntBindId;

        public GXElement Offset;
        public float[] FloatObjects;
        public int[] IntObjects;

        private void InitFloatArray(VertexObjectArrays gXVertexObject)
        {
            List<float> value = new();
            if (gXVertexObject.PositionMatrix.Count > 0)
            {
                Offset.PositionMatrix = value.Count();
                foreach (var posmtx in gXVertexObject.PositionMatrix)
                {
                    value.Add(posmtx.M11);
                    value.Add(posmtx.M12);
                    value.Add(posmtx.M13);
                    value.Add(posmtx.M14);

                    value.Add(posmtx.M21);
                    value.Add(posmtx.M22);
                    value.Add(posmtx.M23);
                    value.Add(posmtx.M24);

                    value.Add(posmtx.M31);
                    value.Add(posmtx.M32);
                    value.Add(posmtx.M33);
                    value.Add(posmtx.M34);

                    value.Add(posmtx.M41);
                    value.Add(posmtx.M42);
                    value.Add(posmtx.M43);
                    value.Add(posmtx.M44);
                }
            }
            for (var i = 0; i < gXVertexObject.TexMatrix.Count(); i++)
            {
                var texmats = gXVertexObject.TexMatrix[i];
                if (texmats.Count > 0)
                {
                    Offset.TexMatrix[i] = value.Count();
                    foreach (var texmat in texmats)
                    {
                        value.Add(texmat.M11);
                        value.Add(texmat.M12);

                        value.Add(texmat.M21);
                        value.Add(texmat.M22);
                    }
                }
            }
            if (gXVertexObject.Position.Count > 0)
            {
                Offset.Position = value.Count();
                foreach (var pos in gXVertexObject.Position)
                {
                    value.Add(pos.X);
                    value.Add(pos.Y);
                    value.Add(pos.Z);
                    value.Add(pos.W);
                }
            }
            if (gXVertexObject.NBT.Count > 0)
            {
                Offset.NBT = value.Count();
                foreach (var nbt in gXVertexObject.NBT)
                {
                    value.Add(nbt.M11);
                    value.Add(nbt.M12);
                    value.Add(nbt.M13);

                    value.Add(nbt.M21);
                    value.Add(nbt.M22);
                    value.Add(nbt.M23);

                    value.Add(nbt.M31);
                    value.Add(nbt.M32);
                    value.Add(nbt.M33);
                }
            }
            for (var i = 0; i < gXVertexObject.TexCoord.Count(); i++)
            {
                var texcoords = gXVertexObject.TexCoord[i];
                if (texcoords.Count > 0)
                {
                    Offset.TexMatrix[i] = value.Count();
                    foreach (var texcoord in texcoords)
                    {
                        value.Add(texcoord.X);
                        value.Add(texcoord.Y);
                        value.Add(texcoord.Z);
                    }
                }
            }

            FloatObjects = value.ToArray();
        }

        private void InitIntArray(VertexObjectArrays gXVertexObject)
        {
            List<int> value = new();

            for (int i = 0; i < gXVertexObject.Color.Length; i++)
            {
                var colors = gXVertexObject.Color[i];
                if (colors.Count > 0)
                {
                    Offset.Color[i] = value.Count();
                    foreach (var color in colors)
                    {
                        value.Add(color[0]);
                        value.Add(color[1]);
                        value.Add(color[2]);
                        value.Add(color[3]);
                    }
                }
            }

            IntObjects = value.ToArray();
        }

        public GXVertexBufferObject(VertexObjectArrays gXVertexObject)
        {
            _disposed = false;

            Offset = new GXElement();
            FloatObjects = [];
            IntObjects = [];

            InitFloatArray(gXVertexObject);
            InitIntArray(gXVertexObject);

            GLBufTarget = BufferTarget.ShaderStorageBuffer;
            GL.GenBuffers(2, [GLFloatBufId, GLIntBufId]);

            GL.BindBuffer(GLBufTarget, GLFloatBufId);
            GL.BufferData(GLBufTarget, sizeof(float) * FloatObjects.Length, FloatObjects, BufferUsageHint.StaticDraw);

            GL.BindBuffer(GLBufTarget, GLIntBufId);
            GL.BufferData(GLBufTarget, sizeof(int) * IntObjects.Length, IntObjects, BufferUsageHint.StaticDraw);

            GL.BindBuffer(GLBufTarget, 0);
        }
        ~GXVertexBufferObject()
        {
            Debug.Assert(_disposed, "OpenTKのリソースを利用したコードではデリータが呼ばれる前にDispose()を呼ぶ必要があります。");
        }

        public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int beginIndex = 0)
        {
            GLFloatBindId = beginIndex;
            GLIntBindId = beginIndex + 1;

            builder.AppendLine($"layout(std430, binding = {GLFloatBindId}) readonly buffer FloatVertex {{");
            builder.AppendLine($"    float Data[];");
            builder.AppendLine($"}} FloatVertex;");
            builder.AppendLine($"layout(std430, binding = {GLIntBindId}) readonly buffer IntVertex {{");
            builder.AppendLine($"    int Data[];");
            builder.AppendLine($"}} IntVertex;");
            builder.AppendLine(string.Empty);

            builder.AppendLine(GLSLInitVertex);
            builder.AppendLine(string.Empty);

            return beginIndex + 1 + 1;
        }

        public void Use()
        {
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, GLFloatBindId, GLFloatBufId);
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, GLIntBindId, GLIntBufId);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            GL.DeleteBuffer(GLIntBufId);
            GL.DeleteBuffer(GLFloatBufId);
        }
    }
}
