using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace TwoBRenn.Engine.Core.Render.ShaderPrograms
{
    abstract class ShaderAttribute
    {
        public abstract void Uniform(int location);

        public static ShaderAttribute Value(Color color) => new ShaderAttribute_Vector4(new Vector4(color.R, color.G, color.B, color.A));
        public static ShaderAttribute Value(Vector4 vector) => new ShaderAttribute_Vector4(vector);
        public static ShaderAttribute Value(float x, float y, float z, float w) => new ShaderAttribute_Vector4(new Vector4(x, y, z, w));
    }

    class ShaderAttribute_Vector4 : ShaderAttribute
    {
        Vector4 vector;
        public ShaderAttribute_Vector4(Vector4 vector)
        {
            this.vector = vector;
        }

        public override void Uniform(int location) => GL.Uniform4(location, vector);
    }
}
