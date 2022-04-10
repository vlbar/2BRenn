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
                sceneManager.OnStart();
            };
            RenderControl.OnRenderTransparent += delegate { sceneManager.OnLateUpdate(); };
            RenderControl.OnRender += delegate { sceneManager.OnUpdate(); };
        }
    }
}
