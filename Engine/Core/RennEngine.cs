using TwoBRenn.Engine.Core.Render;

namespace TwoBRenn.Engine.Core
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
