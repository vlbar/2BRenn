using OpenTK.Graphics.OpenGL4;

namespace TwoBRenn.Engine.Core.Render.ShaderPrograms
{
    class ShaderDefinition
    {
        public ShaderType Type { get; set; }
        public string Path { get; set; }

        public ShaderDefinition(ShaderType type, string path)
        {
            Type = type;
            Path = path;
        }
    }
}
