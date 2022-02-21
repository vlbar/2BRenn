using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;

namespace TwoBRenn.Engine.Core.Render
{
    class BaseShaderProgram
    {
        private int programId = 0;
        private List<int> shaders = new List<int>();

        public Dictionary<string, ShaderAttribute> defaultAttributes = new Dictionary<string, ShaderAttribute>();

        public BaseShaderProgram(List<ShaderDefinition> shaderDefinitions)
        {
            programId = GL.CreateProgram();

            foreach (var shaderDef in shaderDefinitions)
            {
                int shaderId = CreateShader(shaderDef.Type, shaderDef.Path);
                shaders.Add(shaderId);
                GL.AttachShader(programId, shaderId);
            }

            GL.LinkProgram(programId);
            GL.GetProgram(programId, GetProgramParameterName.LinkStatus, out int statusCode);
            if (statusCode != (int)All.True)
            {
                string infoLog = GL.GetProgramInfoLog(programId);
                throw new Exception($"Shader program error in {programId}\n{infoLog}");
            }

            foreach (var shaderId in shaders)
            {
                GL.DetachShader(programId, shaderId);
                GL.DeleteShader(shaderId);
            }
        }

        public void ActiveProgram()
        {
            ActiveProgram(defaultAttributes);
        }

        public void ActiveProgram(Dictionary<string, ShaderAttribute> attributes)
        {
            GL.UseProgram(programId);
            UniformAttributes(attributes);
        }

        public void DeactiveProgram() => GL.UseProgram(0);
        public void DeleteProgram() => GL.DeleteProgram(programId);



        public int CreateVertexArray()
        {
            int vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(vertexArray);
            return vertexArray;
        }

        public void DrawVertexArray(int vertexArray, float[] data)
        {
            GL.BindVertexArray(vertexArray);
            GL.DrawArrays(PrimitiveType.Triangles, 0, data.Length / 3);
        }

        public int CreateBuffer(float[] data, BufferTarget target = BufferTarget.ArrayBuffer, BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw)
        {
            int buffer = GL.GenBuffer();
            GL.BindBuffer(target, buffer);
            GL.BufferData(target, data.Length * sizeof(float), data, bufferUsage);
            return buffer;
        }

        public void SetupBuffer(int buffer, int attributeLocation, int size = 3)
        {
            GL.EnableVertexAttribArray(attributeLocation);
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.VertexAttribPointer(attributeLocation, size, VertexAttribPointerType.Float, false, size * sizeof(float), 0);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableVertexAttribArray(attributeLocation);
        }

        public void DeleteBuffer(int buffer, BufferTarget target = BufferTarget.ArrayBuffer)
        {
            GL.BindBuffer(target, 0);
            GL.DeleteBuffer(buffer);
        }

        public int GetAttributeLocation(string name)
        {
            return GL.GetAttribLocation(programId, name);
        }

        public Dictionary<string, ShaderAttribute> GetDefaultShaderAttributes()
        {
            return defaultAttributes;
        }

        public void SetDefaultShaderAttribute(string name, ShaderAttribute value)
        {
            if (defaultAttributes.ContainsKey(name))
            {
                defaultAttributes.Remove(name);
            }
            defaultAttributes.Add(name, value);
        }

        public void SetDefaultShaderAttributes(Dictionary<string, ShaderAttribute> attributes)
        {
            defaultAttributes = attributes;
        }

        private void UniformAttributes(Dictionary<string, ShaderAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                int location = GL.GetUniformLocation(programId, attribute.Key);
                attribute.Value.Uniform(location);
            }
        }

        private int CreateShader(ShaderType shaderType, string path)
        {
            string shader = File.ReadAllText(path);
            int shaderId = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderId, shader);
            GL.CompileShader(shaderId);

            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out int statusCode);
            if (statusCode != (int)All.True)
            {
                string infoLog = GL.GetShaderInfoLog(shaderId);
                throw new Exception($"Shader compile error in {shaderId}\n{infoLog}");
            }

            return shaderId;
        }
    }
}
