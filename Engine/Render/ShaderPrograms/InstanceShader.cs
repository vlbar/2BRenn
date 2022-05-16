using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class InstanceShader : BaseShaderProgram
    {
        public static readonly string[] ModelMatrixAttribute = { "aModelMatrix0", "aModelMatrix1", "aModelMatrix2", "aModelMatrix3" };

        public const string BaseColorUniform = "baseColor";
        public const string OffsetUniform = "offset";
        public const string TilingUniform = "tiling";

        public InstanceShader()
            : base(new List<ShaderDefinition>
            {
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/instanceShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/simpleShader.frag")
            })
        {
            SetDefaultShaderAttribute(BaseColorUniform, ShaderAttribute.Value(Vector4.One));
            SetDefaultShaderAttribute(TilingUniform, ShaderAttribute.Value(1f, 1f));
        }
    }
}
