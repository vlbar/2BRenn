using System.Collections.Generic;
using TwoBRenn.Engine.Core.Render;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Components
{
    class MeshRenderer : Component
    {
        float[] triangleMeshVertices = new float[] { };
        private BaseShaderProgram shaderProgram;
        private Dictionary<string, ShaderAttribute> attributes;
        private bool isDetachedAttributes = false;

        private int vertexArray;
        private int vertexBuffer;

        public void SetShaderProgram(BaseShaderProgram shaderProgram)
        {
            attributes = shaderProgram.GetDefaultShaderAttributes();
            this.shaderProgram = shaderProgram;
        }

        public void SetTriangleMesh(float[] vertices)
        {
            triangleMeshVertices = vertices;

            vertexArray = shaderProgram.CreateVertexArray();
            vertexBuffer = shaderProgram.CreateBuffer(vertices);
            int positionLocation = shaderProgram.GetAttributeLocation("vertexPos");
            shaderProgram.SetupBuffer(vertexBuffer, positionLocation);
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
            shaderProgram.ActiveProgram(attributes);
            shaderProgram.DrawVertexArray(vertexArray, triangleMeshVertices);
            shaderProgram.DeactiveProgram();
        }

        public override void OnUnload()
        {
            shaderProgram.DeleteBuffer(vertexBuffer);
        }
    }
}
