using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    class ParticleShader : BaseShaderProgram
    {
        public const string RotationSizeAttribute = "aRotationSize";
        public const string OffsetAttribute = "aOffset";
        public const string BaseColorAttribute = "aBaseColor";

        public ParticleShader()
            : base(new List<ShaderDefinition>
            {
                new ShaderDefinition(ShaderType.VertexShader, @"Engine/Render/Shaders/particleShader.vert"),
                new ShaderDefinition(ShaderType.FragmentShader, @"Engine/Render/Shaders/particleShader.frag")
            })
        {
            StaticUniforms = new[]
            {
                ProjectionUniform,
                DirectionalLightUniform.ColorUniform,
                DirectionalLightUniform.IntensityUniform,
                DirectionalLightUniform.DirectionUniform,
                DirectionalLightUniform.DiffuseIntensityUniform
            };

            SetDefaultShaderAttribute(BaseColorAttribute, ShaderAttribute.Value(Vector4.One));
        }
    }
}
