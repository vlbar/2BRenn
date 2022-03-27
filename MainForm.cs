using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Render.Textures;

namespace TwoBRenn
{
    public partial class MainForm : Form
    {
        private RennEngine engine = new RennEngine();

        public MainForm()
        {
            InitializeComponent();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            engine.RenderControl.SetupGlControl(glControl);
            engine.RenderControl.OnRender += delegate
            {
                debugInfoLabel.Text = engine.RenderControl.GetDynamicDebugInfo();
            };

            engine.RenderControl.Skybox = new Skybox(new string[] {
                @"Assets\Textures\skybox\right.png",
                @"Assets\Textures\skybox\left.png",
                @"Assets\Textures\skybox\top.png",
                @"Assets\Textures\skybox\bottom.png",
                @"Assets\Textures\skybox\front.png",
                @"Assets\Textures\skybox\back.png"
            });
        }
    }
}
