using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    abstract class ShaderAttribute
    {
        public abstract void Uniform(int location);

        public static ShaderAttribute Value(Color color) => new ShaderAttribute_Vector4(new Vector4(color.R/255.0f, color.G/255.0f, color.B/255.0f, color.A/255.0f));
        public static ShaderAttribute Value(Vector4 vector) => new ShaderAttribute_Vector4(vector);
        public static ShaderAttribute Value(float x, float y, float z, float w) => new ShaderAttribute_Vector4(new Vector4(x, y, z, w));

        public static ShaderAttribute Value(float x, float y) => new ShaderAttribute_Vector2(new Vector2(x, y));
    }

    class ShaderAttribute_Vector4 : ShaderAttribute
    {
        public Vector4 Vector;
        public ShaderAttribute_Vector4(Vector4 vector)
        {
            Vector = vector;
        }

        public override void Uniform(int location) => GL.Uniform4(location, Vector);
    }

    class ShaderAttribute_Vector2 : ShaderAttribute
    {
        Vector2 vector;
        public ShaderAttribute_Vector2(Vector2 vector)
        {
            this.vector = vector;
        }

        public override void Uniform(int location) => GL.Uniform2(location, vector);
    }
}
