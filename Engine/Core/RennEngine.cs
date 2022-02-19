using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TwoBRenn.Engine.Core
{
    class RennEngine
    {
        private SceneManager sceneManager = new SceneManager();
        public Renderer Renderer { get; } = new Renderer();

        public RennEngine()
        {
            Renderer.AddRenderAction(delegate
            {
                sceneManager.OnUpdate();
            });
        }
    }
}
