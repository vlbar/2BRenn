using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.Utils
{
    public class BufferObject
    {
        private readonly int bufferId;
        private readonly BufferTarget bufferTarget;

        private int subDataOffset = 0;
        private readonly List<int> subDataLocations = new List<int>();

        public BufferObject(BufferTarget target)
        {
            bufferId = GL.GenBuffer();
            bufferTarget = target;
        }

        public void Bind() => GL.BindBuffer(bufferTarget, bufferId);
        public void Unbind()
        {
            subDataLocations.ForEach(GL.DisableVertexAttribArray);
            GL.BindBuffer(bufferTarget, 0);
        }

        public void Delete() => GL.DeleteBuffer(bufferId);

        public void SetData<T>(T[] data, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) where T : struct
        {
            if (data == null || data.Length == 0) return;
            GL.BufferData(bufferTarget, data.Length * Marshal.SizeOf(data[0]), data, usageHint);
        }

        public void SetSubDataSize(int size, BufferUsageHint usageHint = BufferUsageHint.StaticDraw)
        {
            GL.BufferData(bufferTarget, size, (IntPtr)null, BufferUsageHint.StaticDraw);
        }

        public void SetSubData<T>(T[] data, int stride, int location) where T : struct
        {
            if (data == null || data.Length == 0) return;
            int size = data.Length * Marshal.SizeOf(data[0]);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)subDataOffset, (IntPtr)size, data);

            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, stride, VertexAttribPointerType.Float, false, stride * Marshal.SizeOf(data[0]), subDataOffset);

            subDataLocations.Add(location);
            subDataOffset += size;
        }
    }
}