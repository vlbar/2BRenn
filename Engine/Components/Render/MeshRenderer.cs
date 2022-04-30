using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
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
        private BaseShaderProgram shaderProgram;
        private Texture texture;
        private Dictionary<string, ShaderAttribute> attributes = new Dictionary<string, ShaderAttribute>();
        private bool isDetachedAttributes;

        private VertexArrayObject vertexArray = new VertexArrayObject();
        private BufferObject vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
        private BufferObject elementBuffer = new BufferObject(BufferTarget.ElementArrayBuffer);

        private int subDataOffset;

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

        private void BindVertex()
        {
            if (Mesh?.Vertices == null) return;

            vertexArray = new VertexArrayObject();
            vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
            elementBuffer = new BufferObject(BufferTarget.ElementArrayBuffer);

            int positionLocation = shaderProgram.GetAttributeLocation("aVertexPos");
            int texCoordsLocation = shaderProgram.GetAttributeLocation("aTexCoords");

            vertexArray.Bind();

            vertexBuffer.InitializeData(Mesh.DataArraySize);
            SetData(Mesh.VerticesArray, 3, positionLocation);
            SetData(Mesh.UVsArray, 2, texCoordsLocation);

            elementBuffer.SetData(Mesh.Triangles);

            vertexArray.Unbind();
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
            if (Mesh != null && shaderProgram != null) BindVertex();
        }

        public override void OnUpdate()
        {
            if (Mesh?.Vertices == null) return;
            shaderProgram.ActiveProgram(attributes);
            shaderProgram.SetMatrix4(BaseShaderProgram.ModelAttribute, rennObject.Transform.GetGlobalModelMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.ViewAttribute, Camera.GetViewMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.ProjectionAttribute, Camera.GetProjectionMatrix());
            Lighting.FillShaderProgram(shaderProgram);

            texture?.Use();
            vertexArray.Draw(Mesh.Triangles.Length);

            shaderProgram.DeactiveProgram();
        }

        public override void OnUnload()
        {
            //vertexBuffer.Delete();
            //elementBuffer.Delete();
        }

        private void SetData<T>(T[] data, int size, int location) where T : struct
        {
            if (data == null || data.Length == 0) return;
            int length = data.Length * Marshal.SizeOf(data[0]);
            vertexBuffer.SetSubData(data, subDataOffset);
            vertexArray.SetDataPointer(location, size, 0, subDataOffset);
            subDataOffset += length;
        }
    }
}
