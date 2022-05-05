using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class SimpleShader : BaseShaderProgram
    {
        public static string BaseColorUniform = "baseColor";
        public static string OffsetUniform = "offset";
        public static string TilingUniform = "tiling";

        public SimpleShader() 
            : base(new List<ShaderDefinition>() { 
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/simpleShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/simpleShader.frag")
            })
        {
            SetDefaultShaderAttribute(BaseColorUniform, ShaderAttribute.Value(Vector4.One));
            SetDefaultShaderAttribute(TilingUniform, ShaderAttribute.Value(1f, 1f));
        }
    }
}
