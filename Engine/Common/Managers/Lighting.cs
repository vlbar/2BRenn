using System.Drawing;
using OpenTK;
using TwoBRenn.Engine.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Common.Managers
{
    struct DirectionalLight
    {
        public Color Color;
        public float Intensity;
        public Vector3 Direction;
        public float DiffuseIntensity;
    }

    class Lighting
    {
        private static Lighting _instance;
        public static Lighting Instance => _instance ?? (_instance = new Lighting());

        private DirectionalLight directionalLight;

        public void Setup()
        {
            directionalLight.Color = Color.White;
            directionalLight.Intensity = 0.6f;
            directionalLight.Direction = new Vector3(-0.2f, -1.0f, -0.3f);
            directionalLight.DiffuseIntensity = 0.5f;
        }

        public static void FillShaderProgram(BaseShaderProgram shaderProgram)
        {
            Vector3 colorVector =
                new Vector3(DirectionalLight.Color.R / 255.0f, DirectionalLight.Color.G / 255.0f, DirectionalLight.Color.B / 255.0f);
            shaderProgram.SetVector3(BaseShaderProgram.DirectionalLightUniform.ColorUniform, colorVector);
            shaderProgram.SetFloat(BaseShaderProgram.DirectionalLightUniform.IntensityUniform, DirectionalLight.Intensity);
            shaderProgram.SetVector3(BaseShaderProgram.DirectionalLightUniform.DirectionUniform, DirectionalLight.Direction);
            shaderProgram.SetFloat(BaseShaderProgram.DirectionalLightUniform.DiffuseIntensityUniform, DirectionalLight.DiffuseIntensity);
        }

        public static DirectionalLight DirectionalLight => Instance.directionalLight;
    }
}