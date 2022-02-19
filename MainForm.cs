using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TwoBRenn.Engine.Core;

namespace TwoBRenn
{
    public partial class MainForm : Form
    {
        private RennEngine engine = new RennEngine();

        public MainForm()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            engine.Renderer.SetupGlControl(glControl);
            engine.Renderer.AddRenderAction(delegate
            {
                debugInfoLabel.Text = engine.Renderer.GetDynamicDebugInfo();
            });
        }
    }
}
