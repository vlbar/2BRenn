using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Utils;
using TwoBRenn.ObjectsSetups.MeshFactories;

namespace TwoBRenn.Engine.Common.Managers
{
    class Shadows
    {
        private static Shadows _instance;
        public static Shadows Instance => _instance ?? (_instance = new Shadows());

        private FrameBufferObject frameBufferObject;
        private readonly List<MeshRenderer> meshes = new List<MeshRenderer>();
        private float shadowsViewSize;
        private int mapWidth;
        private int mapHeight;
        private Matrix4 lightSpaceMatrix;

        private Mesh debugCube;
        private readonly ShadowsShader shader = new ShadowsShader();
        private readonly DwarfishShader fullShader = new DwarfishShader();

        public void Setup(int shadowMapWidth = 1024, int shadowMapHeight = 1024, float shadowsDistance = 40)
        {
            frameBufferObject = new FrameBufferObject(shadowMapWidth, shadowMapHeight);
            shadowsViewSize = shadowsDistance * 2;
            mapWidth = shadowMapWidth;
            mapHeight = shadowMapHeight;
        }

        public void CalculateShadowsMap()
        {
            shader.ActiveProgram();
            Matrix4 viewMatrix = Camera.GetViewMatrix();
            Vector3 position = viewMatrix.Inverted().ExtractTranslation();
            Quaternion rotation = viewMatrix.Inverted().ExtractRotation();
            position += rotation * (-Vector3.UnitZ * shadowsViewSize * 0.5f);
            position.Y = 0;

            Matrix4 view = Matrix4.LookAt(position + Lighting.DirectionalLight.Direction * -20, position + Vector3.Zero, Vector3.UnitY);
            Matrix4 projection = Matrix4.CreateOrthographic(shadowsViewSize, shadowsViewSize, 0, 100);
            lightSpaceMatrix = view * projection;
            shader.SetMatrix4(BaseShaderProgram.LightSpaceMatrixUniform, lightSpaceMatrix);
            
            GL.Viewport(0, 0, mapWidth, mapHeight);
            frameBufferObject.Write();
            GL.Clear(ClearBufferMask.DepthBufferBit);
            meshes.ForEach(x => x.DrawWithShader(shader));
            frameBufferObject.Unbind();
        }

        public void DebugRender()
        {
            if (debugCube == null)
            {
                debugCube = PrimitiveMeshFactory.CreateCube();
            }

            Matrix4 model = Matrix4.CreateTranslation(0, 1f, -1f);
            Matrix4 view = Matrix4.LookAt(Vector3.UnitX, Vector3.Zero, Vector3.UnitY);
            Matrix4 projection = Matrix4.CreateOrthographic(3, 3, 0, 1);

            fullShader.ActiveProgram();
            UseShadowMap(TextureUnit.Texture0);
            fullShader.SetMatrix4(BaseShaderProgram.ModelUniform, model);
            fullShader.SetMatrix4(BaseShaderProgram.ViewUniform, view);
            fullShader.SetMatrix4(BaseShaderProgram.ProjectionUniform, projection);
            debugCube.Draw(fullShader);
        }               

        public void UseShadowMap(TextureUnit textureUnit)
        {
            frameBufferObject.Read(textureUnit);
        }

        public static void AddMeshRenderer(MeshRenderer meshRenderer)
        {
            meshRenderer.Mesh.InitMeshVertexData(Instance.shader);
            Instance.meshes.Add(meshRenderer);
        }

        public static void FillUniforms(BaseShaderProgram shaderProgram, TextureUnit textureUnit = TextureUnit.Texture1)
        {
            shaderProgram.SetMatrix4(BaseShaderProgram.LightSpaceMatrixUniform, Instance.lightSpaceMatrix);
            Instance.UseShadowMap(textureUnit);
        }
    }
}
