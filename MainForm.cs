using System;
using System.Windows.Forms;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn
{
    public partial class MainForm : Form
    {
        private readonly RennEngine engine = new RennEngine();

        public MainForm()
        {
            InitializeComponent();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            engine.GlControl = glControl;
            engine.Form = this;

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

            SecurityStructPlacerSetup securityStructPlacer = new SecurityStructPlacerSetup();
            engine.ObjectPlacer.ObjectsCreators = securityStructPlacer.GetObjectCreators();
        }
    }
}
