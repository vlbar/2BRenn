using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace TwoBRenn.Engine.Core.Render.ShaderPrograms
{
    class SimpleShader : BaseShaderProgram
    {
        public static string BASE_COLOR = "baseColor";
        public static string OFFSET = "offset";
        public static string TILING = "tiling";

        public SimpleShader() 
            : base(new List<ShaderDefinition>() { 
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Core/Render/Shaders/baseVertexShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Core/Render/Shaders/baseFragmentShader.frag")
            })
        {
            SetDefaultShaderAttribute(BASE_COLOR, ShaderAttribute.Value(Vector4.One));
            SetDefaultShaderAttribute(TILING, ShaderAttribute.Value(1f, 1f));
        }
    }
}
