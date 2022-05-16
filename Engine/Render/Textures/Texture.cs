using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.Textures
{
    public class Texture
    {
        private readonly int texture;
        public Bitmap Image;

        public Texture(string path)
        {
            texture = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            Image = new Bitmap(path); // FIXME: cringe

            using (var image = new Bitmap(path))
            {
                GenerateTexture(image);
            }
        }

        public Texture(Bitmap image)
        {
            texture = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            Image = new Bitmap(image); // FIXME: cringe
            GenerateTexture(image);
        }

        private void GenerateTexture(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                image.Width,
                image.Height,
                0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                image.Width, image.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use(TextureUnit textureUnit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }
    }
}
