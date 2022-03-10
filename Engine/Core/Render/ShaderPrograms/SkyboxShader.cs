using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace TwoBRenn.Engine.Core.Render.ShaderPrograms
{
    class SkyboxShader : BaseShaderProgram
    {
        public static string SKYBOX = "skybox";

        public SkyboxShader()
            : base(new List<ShaderDefinition>() {
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Core/Render/Shaders/skyboxShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Core/Render/Shaders/skyboxShader.frag")
            })
        { }
    }
}
