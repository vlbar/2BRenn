using System.Windows.Forms;
using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Common.ObjectControl;
using TwoBRenn.Engine.Render;
using TwoBRenn.Engine.Scene;

namespace TwoBRenn.Engine
{
    class RennEngine
    {
        public Form Form { set; get; }
        public GLControl GlControl { set; get; }

        private SceneManager sceneManager;
        public ObjectPlacer ObjectPlacer = new ObjectPlacer();
        public RenderControl RenderControl { get; } = new RenderControl();

        public RennEngine()
        {
            RenderControl.OnSetup += delegate
            {
                sceneManager = new SceneManager();
                ObjectPlacer.SceneManager = sceneManager;
                sceneManager.OnStart();

                InputManager.GlControl = GlControl;
                InputManager.Form = Form;
            };
            RenderControl.OnRenderTransparent += delegate { sceneManager.OnLateUpdate(); };
            RenderControl.OnRender += delegate
            {
                sceneManager.OnUpdate();
                ObjectPlacer.OnUpdate();
            };
        }
    }
}
