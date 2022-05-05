using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Render.ShaderPrograms
{
    public class DirectionalLightStruct
    {
        public string ColorUniform = "directionalLight.color";
        public string IntensityUniform = "directionalLight.intensity";
        public string DirectionUniform = "directionalLight.direction";
        public string DiffuseIntensityUniform = "directionalLight.diffuseIntensity";
    }

    class BaseShaderProgram
    {
        public static string VertexPositionAttribute = "aVertexPos";
        public static string TextureCoordinatesAttribute = "aTexCoords";
        public static string VertexNormalAttribute = "aNormal";
        public static string ModelUniform = "model";
        public static string ViewUniform = "view";
        public static string ProjectionUniform = "projection";
        public static DirectionalLightStruct DirectionalLightUniform = new DirectionalLightStruct();

        public readonly int ProgramId;
        private readonly List<int> shaders = new List<int>();

        public Dictionary<string, ShaderAttribute> DefaultAttributes = new Dictionary<string, ShaderAttribute>();

        private readonly Dictionary<string, int> attributeLocations = new Dictionary<string, int>();
        private readonly Dictionary<string, int> uniformLocations = new Dictionary<string, int>();
        protected string[] StaticUniforms;
        private bool[] isStaticUniformApplied;

        public BaseShaderProgram(List<ShaderDefinition> shaderDefinitions)
        {
            ProgramId = GL.CreateProgram();

            foreach (var shaderDef in shaderDefinitions)
            {
                int shaderId = CreateShader(shaderDef.Type, shaderDef.Path);
                shaders.Add(shaderId);
                GL.AttachShader(ProgramId, shaderId);
            }

            GL.LinkProgram(ProgramId);
            GL.GetProgram(ProgramId, GetProgramParameterName.LinkStatus, out int statusCode);
            if (statusCode != (int)All.True)
            {
                string infoLog = GL.GetProgramInfoLog(ProgramId);
                throw new Exception($"Shader program error in {ProgramId}\n{infoLog}");
            }

            foreach (var shaderId in shaders)
            {
                GL.DetachShader(ProgramId, shaderId);
                GL.DeleteShader(shaderId);
            }
        }

        // program
        public void ActiveProgram()
        {
            ActiveProgram(DefaultAttributes);
        }

        public void ActiveProgram(Dictionary<string, ShaderAttribute> attributes)
        {
            if (RennEngine.Instance.RenderControl.CurrentShaderProgram != ProgramId)
            {
                GL.UseProgram(ProgramId);
                RennEngine.Instance.RenderControl.CurrentShaderProgram = ProgramId;
            }

            UniformAttributes(attributes);
        }

        public void DeactiveProgram()
        {
            
        }

        public void DeleteProgram() => GL.DeleteProgram(ProgramId);

        // attributes
        public int GetAttributeLocation(string name)
        {
            if (attributeLocations.ContainsKey(name))
            {
                return attributeLocations[name];
            }
            int location = GL.GetAttribLocation(ProgramId, name);
            attributeLocations.Add(name, location);
            return location;
        }

        public int GetUniformLocation(string name)
        {
            if (uniformLocations.ContainsKey(name))
            {
                return uniformLocations[name];
            }
            int location = GL.GetUniformLocation(ProgramId, name);
            uniformLocations.Add(name, location);
            return location;
        }

        public Dictionary<string, ShaderAttribute> GetDefaultShaderAttributes()
        {
            return DefaultAttributes;
        }

        public void SetDefaultShaderAttribute(string name, ShaderAttribute value)
        {
            if (DefaultAttributes.ContainsKey(name))
            {
                DefaultAttributes.Remove(name);
            }
            DefaultAttributes.Add(name, value);
        }

        public void SetDefaultShaderAttributes(Dictionary<string, ShaderAttribute> attributes)
        {
            DefaultAttributes = attributes;
        }

        private void UniformAttributes(Dictionary<string, ShaderAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                int location = GetUniformLocation(attribute.Key);
                attribute.Value.Uniform(location);
            }
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            if (!IsStaticUniformSetted(name))
            {
                GL.UniformMatrix4(GetUniformLocation(name), false, ref data);
            }
        }

        public void SetVector3(string name, Vector3 vector)
        {
            if (!IsStaticUniformSetted(name))
            {
                GL.Uniform3(GetUniformLocation(name), vector);
            }
        }

        public void SetInt(string name, int value)
        {
            if (!IsStaticUniformSetted(name))
            {
                GL.Uniform1(GetUniformLocation(name), value);
            }
        }

        public void SetFloat(string name, float value)
        {
            if (!IsStaticUniformSetted(name))
            {
                GL.Uniform1(GetUniformLocation(name), value);
            }
        }

        public void CancelStaticApplied()
        {
            for (int i = 0; i < isStaticUniformApplied.Length; i++)
            {
                isStaticUniformApplied[i] = false;
            }
        }

        // utils
        private bool IsStaticUniformSetted(string name)
        {
            if (StaticUniforms != null)
            {
                if (isStaticUniformApplied == null)
                {
                    isStaticUniformApplied = new bool[StaticUniforms.Length];
                }

                int index = Array.IndexOf(StaticUniforms, name);
                if (index >= 0)
                {
                    if (!isStaticUniformApplied[index])
                    {
                        isStaticUniformApplied[index] = true;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
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
