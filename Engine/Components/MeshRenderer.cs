using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components
{
    class MeshRenderer : Component
    {
        private Mesh mesh;
        private BaseShaderProgram shaderProgram;
        private Texture texture;
        private Dictionary<string, ShaderAttribute> attributes;
        private bool isDetachedAttributes;

        private VertexArrayObject vertexArray = new VertexArrayObject();
        private BufferObject vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
        private BufferObject elementBuffer = new BufferObject(BufferTarget.ElementArrayBuffer);

        private int subDataOffset;

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

            vertexBuffer.InitializeData(mesh.DataArraySize);
            SetData(mesh.VerticesArray, 3, positionLocation);
            SetData(mesh.UVsArray, 2, texCoordsLocation);

            elementBuffer.SetData(mesh.Triangles);

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

        public override void OnUpdate()
        {
            if (mesh?.Vertices == null) return;
            shaderProgram.ActiveProgram(attributes);
            shaderProgram.SetMatrix4(BaseShaderProgram.MODEL, rennObject.Transform.GetGlobalModelMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.VIEW, Camera.GetViewMatrix());
            shaderProgram.SetMatrix4(BaseShaderProgram.PROJECTION, Camera.GetProjectionMatrix());

            texture?.Use();
            vertexArray.Draw(mesh.Triangles.Length);

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
