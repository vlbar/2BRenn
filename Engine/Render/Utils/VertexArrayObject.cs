using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.Utils
{
    public class VertexArrayObject
    {
        private readonly int vertexArrayId;

        public VertexArrayObject() => vertexArrayId = GL.GenVertexArray();
        public void Bind() => GL.BindVertexArray(vertexArrayId);
        public void Unbind() => GL.BindVertexArray(0);

        public void SetPointer(int location, int size, int stride, int offset = 0, bool normalized = false,
            VertexAttribPointerType type = VertexAttribPointerType.Float)
        {
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, size, type, normalized, stride, offset);
            GL.EnableVertexAttribArray(0);
        }

        public void SetDivisor(int location, int divisor)
        {
            GL.VertexAttribDivisor(location, divisor);
        }
    }
}