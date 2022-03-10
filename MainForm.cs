using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TwoBRenn.Engine.Core;
using TwoBRenn.Engine.Core.Render;

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
                @"Textures\skybox\right.png",
                @"Textures\skybox\left.png",
                @"Textures\skybox\top.png",
                @"Textures\skybox\bottom.png",
                @"Textures\skybox\front.png",
                @"Textures\skybox\back.png"
            });
        }
    }
}
