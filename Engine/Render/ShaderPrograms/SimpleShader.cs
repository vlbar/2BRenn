using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class SimpleShader : BaseShaderProgram
    {
        public static string BASE_COLOR = "baseColor";
        public static string OFFSET = "offset";
        public static string TILING = "tiling";

        public SimpleShader() 
            : base(new List<ShaderDefinition>() { 
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/baseVertexShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/baseFragmentShader.frag")
            })
        {
            SetDefaultShaderAttribute(BASE_COLOR, ShaderAttribute.Value(Vector4.One));
            SetDefaultShaderAttribute(TILING, ShaderAttribute.Value(1f, 1f));
        }
    }
}
