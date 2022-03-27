using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class SkyboxShader : BaseShaderProgram
    {
        public static string SKYBOX = "skybox";

        public SkyboxShader()
            : base(new List<ShaderDefinition>() {
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/skyboxShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/skyboxShader.frag")
            })
        { }
    }
}
