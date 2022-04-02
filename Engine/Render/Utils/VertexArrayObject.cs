﻿using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.Utils
{
    public class VertexArrayObject
    {
        private readonly int vertexArrayId;

        public VertexArrayObject() => vertexArrayId = GL.GenVertexArray();
        public void Bind() => GL.BindVertexArray(vertexArrayId);
        public void Unbind() => GL.BindVertexArray(0);
    }
}