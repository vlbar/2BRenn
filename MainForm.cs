using System;
using System.Windows.Forms;
using OpenTK;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn
{
    public partial class MainForm : Form
    {
        private readonly RennEngine engine = RennEngine.Instance;
        private readonly GLControl smoothGlControl;

        public MainForm()
        {
            InitializeComponent();

            smoothGlControl = new GLControl(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 2));
            smoothGlControl.Dock = glControl.Dock;
            smoothGlControl.Location = glControl.Location;
            smoothGlControl.Size = glControl.Size;
            smoothGlControl.TabIndex = glControl.TabIndex;
            smoothGlControl.Load += glControl_Load;

            Controls.Remove(glControl);
            Controls.Add(smoothGlControl);
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            
            engine.Setup(smoothGlControl, this);
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
