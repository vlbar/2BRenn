using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class DwarfishShader : BaseShaderProgram
    {
        public DwarfishShader() : base(new List<ShaderDefinition>
        {
            new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/DwarfishShader.vert"),
            new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/DwarfishShader.frag")
        }) { }
    }
}
