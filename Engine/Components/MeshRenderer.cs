using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using TwoBRenn.Engine.Core.Render;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Components
{
    class MeshRenderer : Component
    {
        float[] triangleVertices;
        uint[] vertexIndexes;

        private BaseShaderProgram shaderProgram;
        private Dictionary<string, ShaderAttribute> attributes;
        private bool isDetachedAttributes = false;

        private int vertexArray;
        private int vertexBuffer;

        private int elementBuffer;

        public void SetShaderProgram(BaseShaderProgram shaderProgram)
        {
            attributes = shaderProgram.GetDefaultShaderAttributes();
            this.shaderProgram = shaderProgram;
        }

        public void SetTriangleMesh(float[] vertices, uint[] indexes)
        {
            triangleVertices = vertices;
            vertexIndexes = indexes;

            // vertex array
            vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(vertexArray);

            // vertex buffer
            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, triangleVertices.Length * sizeof(float), triangleVertices, BufferUsageHint.StaticDraw);
            int positionLocation = shaderProgram.GetAttributeLocation("vertexPos");

            GL.EnableVertexAttribArray(positionLocation);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            // element buffer
            elementBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndexes.Length * sizeof(uint), vertexIndexes, BufferUsageHint.StaticDraw);

            // unload
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableVertexAttribArray(positionLocation);
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
            if (triangleVertices == null || vertexIndexes == null) return;

            shaderProgram.ActiveProgram(attributes);
            GL.BindVertexArray(vertexArray);
            GL.DrawElements(PrimitiveType.Triangles, vertexIndexes.Length, DrawElementsType.UnsignedInt, 0);
            shaderProgram.DeactiveProgram();
        }

        public override void OnUnload()
        {
            GL.DeleteBuffer(vertexBuffer);
            GL.DeleteBuffer(elementBuffer);
        }
    }
}
