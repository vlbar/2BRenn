using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace TwoBRenn.Engine.Core.Render.ShaderPrograms
{
    class SimpleShader : BaseShaderProgram
    {
        public static string BASE_COLOR = "baseColor";

        public SimpleShader() 
            : base(new List<ShaderDefinition>() { 
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Core/Render/Shaders/baseVertexShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Core/Render/Shaders/baseFragmentShader.frag")
            })
        { }
    }
}
