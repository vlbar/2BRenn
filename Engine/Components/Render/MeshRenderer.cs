using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Render
{
    class MeshRenderer : Component
    {
        public Mesh Mesh;
        public bool IsCastShadows = true;

        private BaseShaderProgram shaderProgram;
        private Texture texture;
        private Dictionary<string, ShaderAttribute> attributes = new Dictionary<string, ShaderAttribute>();
        private bool isDetachedAttributes;
        
        public void SetShaderProgram(BaseShaderProgram shaderProgram)
        {
            attributes = shaderProgram.GetDefaultShaderAttributes();
            this.shaderProgram = shaderProgram;
        }

        public void SetTexture(Texture texture)
        {
            this.texture = texture;
        }

        public void SetTriangleMesh(Mesh mesh)
        {
            Mesh = mesh;
        }

        public void SetShaderAttribute(string name, ShaderAttribute value)
        {
            if (!isDetachedAttributes)
            {
                attributes = new Dictionary<string, ShaderAttribute>(attributes);
                isDetachedAttributes = true;
            }

            if (attributes.ContainsKey(name))
            {
                attributes.Remove(name);
            }
            attributes.Add(name, value);
        }

        public void SetShaderAttributes(Dictionary<string, ShaderAttribute> attributes)
        {
            this.attributes = attributes;
        }

        public Dictionary<string, ShaderAttribute> GetShaderAttributes() => attributes;

        public Vector4 GetVector4Attribute(string name)
        {
            try
            {
                ShaderAttribute_Vector4 vectorAttribute = (ShaderAttribute_Vector4)attributes[name];
                return new Vector4(vectorAttribute.Vector.X, vectorAttribute.Vector.Y, vectorAttribute.Vector.Z,
                    vectorAttribute.Vector.W);
            }
            catch (Exception)
            {
                return Vector4.Zero;
            }
        }

        public override void OnStart()
        {
            if (Mesh != null && shaderProgram != null)
            {
                Mesh.InitMeshData(shaderProgram);
            }

            if(IsCastShadows) Shadows.AddMeshRenderer(this);
        }

        public override void OnUpdate()
        {
            if (Mesh?.Vertices == null) return;
            shaderProgram.ActiveProgram(attributes);
            shaderProgram.SetMatrix4(BaseShaderProgram.ModelUniform, rennObject.Transform.GetGlobalModelMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.ViewUniform, Camera.GetViewMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.ProjectionUniform, Camera.GetProjectionMatrix());
            Lighting.FillDirectionalLight(shaderProgram);
            Lighting.FillPointLights(shaderProgram);
            Shadows.FillUniforms(shaderProgram);

            texture?.Use();
            Mesh.Draw(shaderProgram);
        }

        public override void OnLateUpdate()
        {
            shaderProgram.CancelStaticApplied();
        }

        public override void OnUnload()
        {

        }

        public void DrawWithShader(BaseShaderProgram shader)
        {
            shader.SetMatrix4(BaseShaderProgram.ModelUniform, rennObject.Transform.GetGlobalModelMatrix());
            Mesh.Draw(shader);
        }
    }
}
