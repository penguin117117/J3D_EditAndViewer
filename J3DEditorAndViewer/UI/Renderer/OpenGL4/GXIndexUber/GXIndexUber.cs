using OpenTK.GLControl;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.OpenGL4.GXIndexUber
{
    internal class GXIndexUber : IRenderer
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public ref Vector3 CameraPosition => throw new NotImplementedException();

        public ref float CameraDistance => throw new NotImplementedException();

        public ref Vector2 CameraAngle => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Resize(int Width, int Height)
        {
            throw new NotImplementedException();
        }

        public void Update(GLControl Surface)
        {
            throw new NotImplementedException();
        }

        // PosMatがMatrix4x3なのは、最終要素が透視投影に使用されるためです。
        const string vertexIndexUberShader = @"
#version 430 core

mat4 PosMat;
mat2 TexMat[8];
vec4 Pos;
vec3 NBT[3];
ivec4 Color[2];
vec4 TexCoord[8];

/// _XtY = X to Y
/// E_ = Element
layout(location =  0) in int  E_PositionMatrix;
layout(location =  1) in ivec E_TexMat0t3;
layout(location =  2) in ivec E_TexMat4t7;
layout(location =  3) in ivec E_Pos_NBT_Color0t1;
layout(location =  4) in ivec E_TexCoord0t3;
layout(location =  5) in ivec E_TexCoord4t7;

/// O_ = Offset
layout(location =  6) in int  O_PositionMatrix;
layout(location =  7) in ivec O_TexMat0t3;
layout(location =  8) in ivec O_TexMat4t7;
layout(location =  9) in ivec O_Pos_NBT_Color0t1;
layout(location = 10) in ivec O_TexCoord0t3;
layout(location = 11) in ivec O_TexCoord4t7;

/// Part = parts
layout(location = 12) in vec Part_ModelPosMat0;
layout(location = 13) in vec Part_ModelPosMat1;
layout(location = 14) in vec Part_ModelPosMat2;

layout(std430, binding = 0) readonly buffer FloatVertex {
    float Data[];
}
layout(std430, binding = 1) readonly buffer IntVertex {
    int Data[];
}

vec3 ArrayToVec3(int Offset, int Index) {
    if (Offset == -1) return vec3(0.0);
    Index = Index * 3;
    int Place = Offset + Index;
    return vec3(
        FloatVertex.Data[Place + 0],
        FloatVertex.Data[Place + 1],
        FloatVertex.Data[Place + 2]);
}
vec4 ArrayToVec4(int Offset, int Index) {
    if (Offset == -1) return vec4(0.0);
    Index = Index * 4;
    int Place = Offset + Index;
    return vec4(
        FloatVertex.Data[Place + 0],
        FloatVertex.Data[Place + 1],
        FloatVertex.Data[Place + 2],
        FloatVertex.Data[Place + 3]);
}
ivec4 ArrayToIVec4(int Offset, int Index) {
    if (Offset == -1) return ivec4(0.0);
    Index = Index * 4;
    int Place = Offset + Index;
    return ivec4(
        IntVertex.Data[Place + 0],
        IntVertex.Data[Place + 1],
        IntVertex.Data[Place + 2],
        IntVertex.Data[Place + 3]);
}
mat2 ArrayToMat2(int Offset, int Index) {
    if (Offset == -1) return mat2(0.0);
    Index = Index * (2 * 2);
    int Place = Offset + Index;
    return mat2(
        FloatVertex.Data[Place + 0],
        FloatVertex.Data[Place + 1],
        FloatVertex.Data[Place + 2],
        FloatVertex.Data[Place + 3]);
}
mat3 ArrayToMat3(int Offset, int Index) {
    if (Offset == -1) return mat3(0.0);
    Index = Index * (3 * 3);
    int Place = Offset + Index;
    return mat3(
        FloatVertex.Data[Place + 0],
        FloatVertex.Data[Place + 1],
        FloatVertex.Data[Place + 2],
        FloatVertex.Data[Place + 3],
        FloatVertex.Data[Place + 4],
        FloatVertex.Data[Place + 5],
        FloatVertex.Data[Place + 6],
        FloatVertex.Data[Place + 7],
        FloatVertex.Data[Place + 8]);
}

void InitTexMat() {
    ivec4 Offset;
    ivec4 Index;

    Offset = O_TexMat0t3;
    Index = E_TexMat0t3;
    TexMat[0] = ArrayToMat2(Offset.x, Index.x);
    TexMat[1] = ArrayToMat2(Offset.y, Index.y);
    TexMat[2] = ArrayToMat2(Offset.z, Index.z);
    TexMat[3] = ArrayToMat2(Offset.w, Index.w);

    Offset = O_TexMat4t7;
    Index = E_TexMat4t7;
    TexMat[4] = ArrayToMat2(Offset.x, Index.x);
    TexMat[5] = ArrayToMat2(Offset.y, Index.y);
    TexMat[6] = ArrayToMat2(Offset.z, Index.z);
    TexMat[7] = ArrayToMat2(Offset.w, Index.w);
};
void InitPos_NBT_Color(){
    ivec4 Offset;
    ivec4 Index;

    Offset = O_Pos_NBT_Color0t1;
    Index = E_Pos_NBT_Color0t1;
    Pos = ArrayToVec4(Offset.x, Index.x);
    mat3 MatNBT = ArrayToMat3(Offset.y, Index.y);
    NBT[0] = MatNBT[0];
    NBT[1] = MatNBT[1];
    NBT[2] = MatNBT[2];
    Color[0] = ArrayToIVec4(Offset.z, Index.z);
    Color[1] = ArrayToIVec4(Offset.w, Index.w);
}
void InitTexCoord(){
    ivec4 Offset;
    ivec4 Index;

    Offset = O_TexCoord0t3;
    Index = E_TexCoord0t3;
    TexCoord[0] = ArrayToIVec4(Offset.x, Index.x);
    TexCoord[1] = ArrayToIVec4(Offset.y, Index.y);
    TexCoord[2] = ArrayToIVec4(Offset.z, Index.z);
    TexCoord[3] = ArrayToIVec4(Offset.w, Index.w);

    Offset = O_TexCoord4t7;
    Index = E_TexCoord4t7;
    TexCoord[4] = ArrayToIVec4(Offset.x, Index.x);
    TexCoord[5] = ArrayToIVec4(Offset.y, Index.y);
    TexCoord[6] = ArrayToIVec4(Offset.z, Index.z);
    TexCoord[7] = ArrayToIVec4(Offset.w, Index.w);
}

void InitVertex() {
    InitTexMat();
    InitPos_NBT_Color();
    InitTexCoord();
}

layout(std430, binding = 2) uniform Projection {
    mat4 CameraMatrix; // Projection * View;
}

out vec4 VertColor;

int main(){
    vec4 PosOut;
    PosOut = vec4(dot(Part_ModelPosMat0, Pos),
                  dot(Part_ModelPosMat0, Pos),
                  dot(Part_ModelPosMat0, Pos),
                  1.0);
    PosOut = CameraMatrix * PosOut;

    gl_Position = PosOut;

    VertColor = Color[0];
}
";
        const string fragmentIndexUberShader = @"
#version 430 core

in vec4 VertColor;

out vec4 FragColor;

int main() {
    FragColor = VertColor;
}
";
    }
}
