using System;
using System.Collections.Generic;
using System.Drawing;
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
        public string Name = "Материал";
        public Texture Texture;
        public Color Color = Color.White;
    }

    class Selectable : Component
    {
        public string Name = "[RennObject]";
        public float BlinkDuration = 1.4f;
        public bool CanChangeTransform = true;
        public List<Material> Materials;

        private ObjectPicker objectPicker;
        private bool isSelected;

        public Vector4 DefaultColor;
        private float blinkAnimation;

        private MeshRenderer meshRenderer;

        public override void OnStart()
        {
            objectPicker = RennEngine.Instance.ObjectPicker;
            meshRenderer = rennObject.GetComponent<MeshRenderer>();
            DefaultColor = meshRenderer.GetVector4Attribute(SimpleShader.BaseColorUniform);
            DefaultColor.W = 1;
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
                meshRenderer.SetShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(DefaultColor));
            }

            if (isSelected)
            {
                blinkAnimation += Time.DeltaTime;
                float limitedBlink = PingPong(blinkAnimation, BlinkDuration);
                float color = limitedBlink / BlinkDuration;
                if (DefaultColor != Vector4.One)
                {
                    float x = DefaultColor.X / 2 + DefaultColor.X / 2 * color;
                    float y = DefaultColor.Y / 2 + DefaultColor.Y / 2 * color;
                    float z = DefaultColor.Z / 2 + DefaultColor.Z / 2 * color;

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
