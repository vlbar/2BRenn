using System;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.Utils
{
    public class VertexArrayObject
    {
        private readonly int vertexArrayId;

        public VertexArrayObject() => vertexArrayId = GL.GenVertexArray();
        public void Bind() => GL.BindVertexArray(vertexArrayId);
        public void Unbind() => GL.BindVertexArray(0);

        public void SetDataPointer(int location, int size, int stride = 0, int offset = 0, bool normalized = false,
            VertexAttribPointerType type = VertexAttribPointerType.Float)
        {
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, size, type, normalized, stride, offset);
        }

        public void SetDivisor(int location, int divisor)
        {
            GL.VertexAttribDivisor(location, divisor);
        }

        public void Draw(int vertexCount, PrimitiveType primitive = PrimitiveType.Triangles, 
            DrawElementsType dataType = DrawElementsType.UnsignedInt, int offset = 0)
        {
            Bind();
            GL.DrawElements(primitive, vertexCount, dataType, offset);
        }

        public void DrawInstanced(int vertexCount, int instanceCount, PrimitiveType primitive = PrimitiveType.Triangles,
            DrawElementsType dataType = DrawElementsType.UnsignedInt, int offset = 0)
        {
            Bind();
            GL.DrawElementsInstanced(primitive, vertexCount, dataType, (IntPtr)offset, instanceCount);
        }

        public void DrawArrayInstanced(int vertexCount, int instanceCount, PrimitiveType primitive = PrimitiveType.Triangles,
            int offset = 0)
        {
            Bind();
            GL.DrawArraysInstanced(primitive, offset, vertexCount, instanceCount);
        }
    }
}