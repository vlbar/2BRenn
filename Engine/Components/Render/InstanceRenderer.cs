using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;
using TwoBRenn.Engine.Utils;

namespace TwoBRenn.Engine.Components.Render
{
    class InstanceRenderer : Component
    {
        public List<Transform> InstanceTransforms;
        public Mesh Mesh;
        public InstanceShader Shader;
        public Texture Texture;

        private float[] transformsArray;
        private readonly VertexArrayObject vertexArray = new VertexArrayObject();
        private readonly BufferObject vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
        private readonly BufferObject modelBuffer = new BufferObject(BufferTarget.ArrayBuffer);
        private readonly BufferObject elementBuffer = new BufferObject(BufferTarget.ElementArrayBuffer);

        private int subDataOffset;

        public override void OnStart()
        {
            if (CanRender()) return;

            int positionLocation = Shader.GetAttributeLocation(InstanceShader.VertexPositionAttribute);
            int texCoordsLocation = Shader.GetAttributeLocation(InstanceShader.TextureCoordinatesAttribute);

            int[] modelMatrixLocation = GetAttributeLocations(Shader, InstanceShader.ModelMatrixAttribute);

            vertexArray.Bind();

            vertexBuffer.InitializeData(
                Mesh.GetMeshDataSize(Mesh.VerticesArray, positionLocation) +
                Mesh.GetMeshDataSize(Mesh.UVsArray, texCoordsLocation));
            SetData(Mesh.VerticesArray, 3, positionLocation);
            vertexArray.SetDivisor(positionLocation, 0);
            SetData(Mesh.UVsArray, 2, texCoordsLocation);
            vertexArray.SetDivisor(texCoordsLocation, 0);

            elementBuffer.SetData(Mesh.Triangles);

            List<float> transformList = new List<float>();
            for (int i = 0; i < InstanceTransforms.Count; i++)
            {
                Matrix4 modelMatrix = InstanceTransforms[i].GetGlobalModelMatrix();
                transformList.AddRange(Vector4ToArray(modelMatrix.Row0));
                transformList.AddRange(Vector4ToArray(modelMatrix.Row1));
                transformList.AddRange(Vector4ToArray(modelMatrix.Row2));
                transformList.AddRange(Vector4ToArray(modelMatrix.Row3));
            }
            transformsArray = transformList.ToArray();

            modelBuffer.InitializeData(transformsArray.Length * sizeof(float));
            modelBuffer.SetData(transformsArray);
            for (int i = 0; i < 4; i++)
            {
                vertexArray.SetDataPointer(modelMatrixLocation[i], 4, 16 * sizeof(float), i * 4 * sizeof(float));
                vertexArray.SetDivisor(modelMatrixLocation[i], 1);
            }

            vertexArray.Unbind();
        }

        public override void OnUpdate()
        {
            if (CanRender()) return;
            Shader.ActiveProgram();
            Shader.SetMatrix4(InstanceShader.ViewUniform, Camera.GetViewMatrix());
            Shader.SetMatrix4(InstanceShader.ProjectionUniform, Camera.GetProjectionMatrix());
            Lighting.FillShaderProgram(Shader);

            Texture?.Use();
            vertexArray.DrawInstanced(Mesh.Triangles.Length, InstanceTransforms.Count);

            Shader.DeactiveProgram();
        }

        private void SetData<T>(T[] data, int size, int location) where T : struct
        {
            if (data == null || data.Length == 0) return;
            int length = data.Length * Marshal.SizeOf(data[0]);
            vertexBuffer.SetSubData(data, subDataOffset);
            vertexArray.SetDataPointer(location, size, 0, subDataOffset);
            subDataOffset += length;
        }

        private int[] GetAttributeLocations(InstanceShader shader, string[] locations)
        {
            List<int> locationList = new List<int>();
            foreach (var location in locations)
            {
                locationList.Add(shader.GetAttributeLocation(location));
            }
            return locationList.ToArray();
        }

        private float[] Vector4ToArray(Vector4 vector)
        {
            return new[] { vector.X, vector.Y, vector.Z, vector.W };
        }

        private bool CanRender()
        {
            return Mesh?.Vertices == null || Shader == null;
        }
    }
}