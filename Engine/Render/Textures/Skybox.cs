using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Render.Textures
{
    class Skybox
    {
        private SkyboxShader shaderProgram = new SkyboxShader();
        private int cubemap;
        private int vertexArray;
        private int vertexBuffer;

        float[] skyboxVertices = {
            -1.0f,  1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,
            
            -1.0f, -1.0f,  1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f, -1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,

             1.0f, -1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f,  1.0f, -1.0f,

            -1.0f, -1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,
             1.0f, -1.0f,  1.0f,
            
            -1.0f,  1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
             1.0f,  1.0f, -1.0f,
             1.0f,  1.0f,  1.0f,
            -1.0f,  1.0f, -1.0f,
            -1.0f,  1.0f,  1.0f,
            
            -1.0f, -1.0f, -1.0f,
             1.0f, -1.0f, -1.0f,
            -1.0f, -1.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,
             1.0f, -1.0f, -1.0f,
             1.0f, -1.0f,  1.0f,
        };

        // right, left, top, bottom, front, back
        public Skybox(string[] facesPaths)
        {
            BindVertex();
            cubemap = CreateCubemapTexture(facesPaths);

            // shader configuration
            shaderProgram.ActiveProgram();
            shaderProgram.SetInt(SkyboxShader.SKYBOX, 0);
            shaderProgram.DeactiveProgram();
        }

        private int CreateCubemapTexture(string[] facesPaths)
        {
            int cubemap = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureCubeMap, cubemap);

            for (int i = 0; i < 6; i++)
            {
                string path = facesPaths[i];
                using (var image = new Bitmap(path))
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    var data = image.LockBits(
                        new Rectangle(0, 0, image.Width, image.Height),
                        ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba,
                        image.Width, image.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                }
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            return cubemap;
        }

        private void BindVertex()
        {
            // vertex array
            vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(vertexArray);

            // vertex buffer
            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, skyboxVertices.Length * sizeof(float), skyboxVertices, BufferUsageHint.StaticDraw);

            int positionLocation = shaderProgram.GetAttributeLocation("aVertexPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            // unload
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableVertexAttribArray(positionLocation);
        }

        public void Use()
        {
            shaderProgram.ActiveProgram();
            shaderProgram.SetMatrix4(SkyboxShader.ViewAttribute, Camera.Camera.GetViewMatrix().ClearTranslation());
            shaderProgram.SetMatrix4(SkyboxShader.ProjectionAttribute, Camera.Camera.GetProjectionMatrix());

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, cubemap);

            GL.DepthFunc(DepthFunction.Lequal);

            GL.BindVertexArray(vertexArray);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            GL.BindVertexArray(0);

            GL.DepthFunc(DepthFunction.Less);
        }
    }
}
