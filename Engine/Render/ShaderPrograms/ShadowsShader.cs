using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class ShadowsShader : BaseShaderProgram
    {
        public ShadowsShader() : base(new List<ShaderDefinition>
        {
            new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/shadowsShader.vert"),
            new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/shadowsShader.frag")
        }) { }
    }
}
