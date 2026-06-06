// OpenTK
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
//
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.UBO;
using J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace J3DEditorAndViewer.UI.Renderer.Resource.OpenGL4.Object.Buffer.SSBO
{
    // WARNING: 実装されない可能性あり。
    //internal class StructSSBOManager<UniformT> : ISSBOManager<UniformT>
    //{
    //    private readonly string DefineName;

    //    private bool disposed = false;

    //    private UniformT data;
    //    public ref UniformT Data
    //    {
    //        get { return ref data; }
    //    }
    //    /// <summary>
    //    /// 型Tのバイト数
    //    /// </summary>
    //    private int SizeInBytes = Marshal.SizeOf<UniformT>();

    //    readonly private int BufferIndex;

    //    public readonly BufferUsageHint BufferUsageHint;

    //    private IAutoObjectBinder CreateObjectBinder()
    //    {
    //        return new AutoShaderStrageBufferBinder(BufferIndex);
    //    }

    //    public int WriteDefinicator(StringBuilder builder, IGLSLTypeTraits typeTrait, int location = 0)
    //    {
    //        var membersInfo = typeof(UniformT).GetFields();
    //        builder.Append($"layout(std140, binding = {location}) uniform {DefineName} {{\n");
    //        GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, location, BufferIndex);
    //        foreach (int I in Enumerable.Range(0, membersInfo.Length))
    //        {
    //            var memberInfo = membersInfo[I];
    //            IGLSLType typeGLSL = typeTrait.TypeOf(memberInfo.FieldType);

    //            builder.Append($"  {typeGLSL.Name} {memberInfo.Name};\n");
    //        }
    //        builder.Append("};\n");
    //        location++;

    //        return location;
    //    }


    //    public StructSSBOManager(string defineName, in UniformT uniformData, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw)
    //    {
    //        data = uniformData;
    //        BufferUsageHint = bufferUsageHint;

    //        // BufferObjectの確保。
    //        BufferIndex = GL.GenBuffer();

    //        using var aob = CreateObjectBinder();
    //        GL.BufferData(BufferTarget.ShaderStorageBuffer, SizeInBytes, 0, BufferUsageHint);

    //        // DefineNameはlocationによる修飾が無い場合に必要。
    //        // そうでなくともコンパイルのために必要になる。
    //        DefineName = defineName;
    //    }
    //    ~StructSSBOManager()
    //    {
    //        if (!disposed)
    //        {
    //            throw new Exception("Dispose が呼ばれていません。");
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        if (disposed) return;
    //        disposed = true;
    //        GL.DeleteBuffer(BufferIndex);
    //    }

    //    private void UpdateBuffer(IAutoObjectBinder aob)
    //    {
    //        GL.BufferSubData(BufferTarget.UniformBuffer, 0, SizeInBytes, ref data);
    //    }
    //    private void UpdateBuffer()
    //    {
    //        using var aob = CreateObjectBinder();
    //        UpdateBuffer(aob);
    //    }

    //    public void Use()
    //    {
    //        UpdateBuffer();
    //    }
    //}
}
