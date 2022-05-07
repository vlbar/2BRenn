using System.Drawing;
using OpenTK;
using TwoBRenn.Engine.Common.Managers;

namespace TwoBRenn.Engine.Components.Light
{
    class PointLight : Component
    {
        public Color Color = Color.White;
        public float AmbientIntensity = 1.0f;
        public float DiffuseIntensity = 0.8f;
        public float ConstantAttenuation = 1.0f;
        public float LinearAttenuation = 0.35f;
        public float QuadraticAttenuation = 0.44f;

        public Vector3 Position => rennObject.Transform.GetGlobalModelMatrix().ExtractTranslation();

        public override void OnStart()
        {
            Lighting.AddPointLight(this);
        }
    }
}
