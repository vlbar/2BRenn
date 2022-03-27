using TwoBRenn.Engine.Render;
using TwoBRenn.Engine.Scene;

namespace TwoBRenn.Engine
{
    class RennEngine
    {
        private SceneManager sceneManager;
        public RenderControl RenderControl { get; } = new RenderControl();

        public RennEngine()
        {
            RenderControl.OnSetup += delegate
            {
                sceneManager = new SceneManager();
            };

            RenderControl.OnRender += delegate
            {
                sceneManager.OnUpdate();
            };
        }
    }
}
