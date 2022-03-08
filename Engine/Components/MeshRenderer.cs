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

        private int vertexArray;
        private int vertexBuffer;
        private int elementBuffer;

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
            // vertex array
            vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(vertexArray);

            // vertex buffer
            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.TriangleVertices.Length * sizeof(float), mesh.TriangleVertices, BufferUsageHint.StaticDraw);

            int positionLocation = shaderProgram.GetAttributeLocation("aVertexPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int texCoordLocation = shaderProgram.GetAttributeLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            // element buffer
            elementBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.VertexIndexes.Length * sizeof(uint), mesh.VertexIndexes, BufferUsageHint.StaticDraw);

            // unload
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableVertexAttribArray(positionLocation);
            GL.DisableVertexAttribArray(texCoordLocation);
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
            if (mesh.TriangleVertices == null || mesh.VertexIndexes == null) return;
            shaderProgram.ActiveProgram(attributes);
            shaderProgram.SetMatrix4(BaseShaderProgram.MODEL, rennObject.Transform.GetGlobalModelMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.VIEW, Camera.GetViewMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.PROJECTION, Camera.GetProjectionMatrix());

            if (texture != null) texture.Use();
            GL.BindVertexArray(vertexArray);
            GL.DrawElements(PrimitiveType.Triangles, mesh.VertexIndexes.Length, DrawElementsType.UnsignedInt, 0);
            shaderProgram.DeactiveProgram();
        }

        public override void OnUnload()
        {
            GL.DeleteBuffer(vertexBuffer);
            GL.DeleteBuffer(elementBuffer);
        }
    }
}
