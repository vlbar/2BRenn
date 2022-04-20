using System;
using System.Windows.Forms;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn
{
    public partial class MainForm : Form
    {
        private readonly RennEngine engine = RennEngine.Instance;

        public MainForm()
        {
            InitializeComponent();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            engine.Setup(glControl, this);
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

            Timer logTimer = new Timer();
            logTimer.Interval = 50;
            logTimer.Tick += delegate { DisplayLogs(); };
            logTimer.Enabled = true;
        }

        private void DisplayLogs()
        {
            debugInfoLabel.Text = engine.DebugManager.GetDynamicDebugInfo();
        }
    }
}
