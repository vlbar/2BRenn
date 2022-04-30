using System.Drawing;
using OpenTK;
using TwoBRenn.Engine.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Common.Managers
{
    struct DirectionalLight
    {
        public Color Color;
        public float Intensity;
    }

    class Lighting
    {
        private static Lighting _instance;
        public static Lighting Instance => _instance ?? (_instance = new Lighting());

        private DirectionalLight directionalLight;

        public void Setup()
        {
            directionalLight.Color = Color.White;
            directionalLight.Intensity = 1f;
        }

        public static void FillShaderProgram(BaseShaderProgram shaderProgram)
        {
            Vector3 colorVector =
                new Vector3(DirectionalLight.Color.R / 255.0f, DirectionalLight.Color.G / 255.0f, DirectionalLight.Color.B / 255.0f);
            shaderProgram.SetVector3(BaseShaderProgram.DirectionalLight.ColorAttribute, colorVector);
            shaderProgram.SetFloat(BaseShaderProgram.DirectionalLight.IntensityAttribute, DirectionalLight.Intensity);
        }

        public static DirectionalLight DirectionalLight => Instance.directionalLight;
    }
}
