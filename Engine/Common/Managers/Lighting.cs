using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using TwoBRenn.Engine.Components.Light;
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
        private List<PointLight> pointLights = new List<PointLight>();

        public void Setup()
        {
            directionalLight.Color = Color.White;
            directionalLight.Intensity = 0.85f;
            directionalLight.Direction = new Vector3(0.2f, -1.0f, -0.3f);
            directionalLight.DiffuseIntensity = 0.5f;
        }

        public static void FillPointLights(BaseShaderProgram shaderProgram)
        {
            int pointLightsCount = 0;
            for (int i = 0; i < Instance.pointLights.Count; i++)
            {
                PointLight pointLight = Instance.pointLights[i];
                if (pointLight.IsEnabled)
                {
                    pointLightsCount++;
                    shaderProgram.SetVector3(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.PositionUniform, pointLight.Position);
                    Vector3 color = new Vector3(pointLight.Color.R / 255.0f, pointLight.Color.G / 255.0f,
                        pointLight.Color.B / 255.0f);
                    shaderProgram.SetVector3(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.ColorUniform, color);
                    shaderProgram.SetFloat(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.AmbientIntensityUniform, pointLight.AmbientIntensity);
                    shaderProgram.SetFloat(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.DiffuseIntensityUniform, pointLight.DiffuseIntensity);

                    shaderProgram.SetFloat(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.ConstantAttenuationUniform, pointLight.ConstantAttenuation);
                    shaderProgram.SetFloat(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.LinearAttenuationUniform, pointLight.LinearAttenuation);
                    shaderProgram.SetFloat(
                        BaseShaderProgram.PointLightsUniform + $"[{i}]." +
                        BaseShaderProgram.PointLightUniform.QuadraticAttenuationUniform, pointLight.QuadraticAttenuation);
                }
            }

            shaderProgram.SetInt(BaseShaderProgram.PointLightsCountUniform, pointLightsCount);
        }

        public static void FillDirectionalLight(BaseShaderProgram shaderProgram)
        {
            Vector3 colorVector =
                new Vector3(DirectionalLight.Color.R / 255.0f, DirectionalLight.Color.G / 255.0f, DirectionalLight.Color.B / 255.0f);
            shaderProgram.SetVector3(BaseShaderProgram.DirectionalLightUniform.ColorUniform, colorVector);
            shaderProgram.SetFloat(BaseShaderProgram.DirectionalLightUniform.IntensityUniform, DirectionalLight.Intensity);
            shaderProgram.SetVector3(BaseShaderProgram.DirectionalLightUniform.DirectionUniform, DirectionalLight.Direction);
            shaderProgram.SetFloat(BaseShaderProgram.DirectionalLightUniform.DiffuseIntensityUniform, DirectionalLight.DiffuseIntensity);
        }

        public static DirectionalLight DirectionalLight => Instance.directionalLight;

        public static void AddPointLight(PointLight pointLight)
        {
            Instance.pointLights.Add(pointLight);
        }
    }
}