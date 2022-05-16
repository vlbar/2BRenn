using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.Utils
{
    public class BufferObject
    {
        private readonly int bufferId;
        private readonly BufferTarget bufferTarget;
        private readonly BufferUsageHint bufferUsage;

        public BufferObject(BufferTarget target, BufferUsageHint usageHint = BufferUsageHint.StaticDraw)
        {
            bufferId = GL.GenBuffer();
            bufferTarget = target;
            bufferUsage = usageHint;
        }

        public void Bind() => GL.BindBuffer(bufferTarget, bufferId);
        public void Unbind() => GL.BindBuffer(bufferTarget, 0);
        public void Delete() => GL.DeleteBuffer(bufferId);

        public void InitializeData(int size)
        {
            Bind();
            GL.BufferData(bufferTarget, size, (IntPtr)null, bufferUsage);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            if (data == null || data.Length == 0) return;
            Bind();
            GL.BufferData(bufferTarget, data.Length * Marshal.SizeOf(data[0]), data, bufferUsage);
        }

        public void SetSubData<T>(T[] data, int offset) where T : struct
        {
            if (data == null || data.Length == 0) return;
            GL.BufferSubData(bufferTarget, (IntPtr)offset, (IntPtr)(data.Length * Marshal.SizeOf(data[0])), data);
        }
    }
}