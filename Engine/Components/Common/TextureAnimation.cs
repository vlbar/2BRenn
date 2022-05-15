using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Common
{
    class TextureAnimation : Component
    {
        public int FrameCount = 10;
        public float Delay = 0.2f;
        private int currentFrame;
        private float time;
        private MeshRenderer renderer;

        public override void OnStart()
        {
            renderer = rennObject.GetComponent<MeshRenderer>();
            renderer.SetShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(1.0f / FrameCount, 1));
        }

        public override void OnUpdate()
        {
            time += Time.DeltaTime;
            if (time > Delay)
            {
                time = 0;
                currentFrame++;
                if (currentFrame >= FrameCount) currentFrame = 0;
                renderer.SetShaderAttribute(SimpleShader.OffsetUniform, ShaderAttribute.Value(1.0f / FrameCount * currentFrame, 0));
            }
        }
    }
}
