using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using TwoBRenn.Engine.Core.Render;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Components
{
    class MeshRenderer : Component
    {
        private Mesh mesh;
        private BaseShaderProgram shaderProgram;
        private Texture texture;
        private Dictionary<string, ShaderAttribute> attributes;
        private bool isDetachedAttributes = false;

        private VertexArrayObject vertexArray = new VertexArrayObject();
        private BufferObject vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
        private BufferObject elementBuffer = new BufferObject(BufferTarget.ElementArrayBuffer);

        public void SetShaderProgram(BaseShaderProgram shaderProgram)
        {
            attributes = shaderProgram.GetDefaultShaderAttributes();
            this.shaderProgram = shaderProgram;
            if (mesh != null) BindVertex();
        }

        public void SetTexture(Texture texture)
        {
            this.texture = texture;
        }

        public void SetTriangleMesh(Mesh mesh)
        {
            this.mesh = mesh;
            if (shaderProgram != null) BindVertex();
        }

        private void BindVertex()
        {
            if (mesh?.Vertices == null) return;

            vertexArray = new VertexArrayObject();
            vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
            elementBuffer = new BufferObject(BufferTarget.ElementArrayBuffer);

            int positionLocation = shaderProgram.GetAttributeLocation("aVertexPos");
            int texCoordsLocation = shaderProgram.GetAttributeLocation("aTexCoords");

            vertexArray.Bind();

            vertexBuffer.Bind();
            vertexBuffer.SetSubDataSize(mesh.DataArraySize);
            vertexBuffer.SetSubData(mesh.VerticesArray, 3, positionLocation);
            vertexBuffer.SetSubData(mesh.UVsArray, 2, texCoordsLocation);

            elementBuffer.Bind();
            elementBuffer.SetData(mesh.Triangles);

            vertexArray.Unbind();
            elementBuffer.Unbind();
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

        public override void OnUpdate()
        {
            if (mesh?.Vertices == null) return;
            shaderProgram.ActiveProgram(attributes);
            shaderProgram.SetMatrix4(BaseShaderProgram.MODEL, rennObject.Transform.GetGlobalModelMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.VIEW, Camera.GetViewMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.PROJECTION, Camera.GetProjectionMatrix());

            if (texture != null) texture.Use();
            vertexArray.Bind();
            GL.DrawElements(PrimitiveType.Triangles, mesh.Triangles.Length, DrawElementsType.UnsignedInt, 0);
            shaderProgram.DeactiveProgram();
        }

        public override void OnUnload()
        {
            vertexBuffer.Delete();
            elementBuffer.Delete();
        }
    }
}
