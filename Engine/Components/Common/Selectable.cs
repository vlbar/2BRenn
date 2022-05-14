using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine.Common.ObjectControl;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Common
{
    class Material
    {
        public string Name;
        public Texture Texture;
    }

    class Selectable : Component
    {
        public string Name = "[RennObject]";
        public float BlinkDuration = 1.4f;
        public bool CanChangeTransform = true;
        public List<Material> Materials;

        private ObjectPicker objectPicker;
        private bool isSelected;

        private Vector4 defaultColor;
        private float blinkAnimation;

        private MeshRenderer meshRenderer;

        public override void OnStart()
        {
            objectPicker = RennEngine.Instance.ObjectPicker;
            meshRenderer = rennObject.GetComponent<MeshRenderer>();
            defaultColor = meshRenderer.GetVector4Attribute(SimpleShader.BaseColorUniform);
            defaultColor.W = 1;
            blinkAnimation = BlinkDuration;
        }

        public override void OnUpdate()
        {
            if (objectPicker.CurrentObject == rennObject)
            {
                isSelected = true;
            }
            else if(isSelected)
            {
                isSelected = false;
                blinkAnimation = BlinkDuration;
                meshRenderer.SetShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(defaultColor));
            }

            if (isSelected)
            {
                blinkAnimation += Time.DeltaTime;
                float limitedBlink = PingPong(blinkAnimation, BlinkDuration);
                float color = limitedBlink / BlinkDuration;
                if (defaultColor != Vector4.One)
                {
                    float x = defaultColor.X / 2 + defaultColor.X / 2 * color;
                    float y = defaultColor.Y / 2 + defaultColor.Y / 2 * color;
                    float z = defaultColor.Z / 2 + defaultColor.Z / 2 * color;

                    meshRenderer.SetShaderAttribute(SimpleShader.BaseColorUniform,
                        ShaderAttribute.Value(x, y, z, 1f));
                }
                else
                {
                    float value = 0.5f + color / 2;
                    meshRenderer.SetShaderAttribute(SimpleShader.BaseColorUniform,
                        ShaderAttribute.Value(value, value, value, 1f));
                }
            }
        }

        public void SetMaterial(int index)
        {
            Material selectedMaterial = Materials[index];
            meshRenderer.SetTexture(selectedMaterial.Texture);
        }

        private static float Repeat(float t, float length)
        {
            return (float)MathHelper.Clamp(t - Math.Floor(t / length) * length, 0.0f, length);
        }

        private static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2F);
            return length - Math.Abs(t - length);
        }
    }
}
