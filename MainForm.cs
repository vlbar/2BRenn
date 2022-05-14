using System;
using System.Collections.Generic;
using System.Drawing;
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

            InitStructuresMenu();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            engine.Setup(smoothGlControl, this);
            engine.RenderControl.Skybox = new Skybox(new string[] {
                @"Assets\Textures\Skybox\right.jpg",
                @"Assets\Textures\Skybox\left.jpg",
                @"Assets\Textures\Skybox\top.jpg",
                @"Assets\Textures\Skybox\bottom.jpg",
                @"Assets\Textures\Skybox\front.jpg",
                @"Assets\Textures\Skybox\back.jpg"
            }, MathHelper.DegreesToRadians(-30));

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

        private void InitStructuresMenu()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(52, 52);
            imageList.Images.Add(new Bitmap(@"Assets\UI\barrier-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\buffer-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\conus-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\fence-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\wheels-icon.jpg"));

            structuresListView.LargeImageList = imageList;

            List<ListViewItem> items = new List<ListViewItem>
            {
                new ListViewItem
                {
                    ImageIndex = 0,
                    Text = "Барьер"
                },
                new ListViewItem
                {
                    ImageIndex = 1,
                    Text = "Буфер"
                },
                new ListViewItem
                {
                    ImageIndex = 2,
                    Text = "Конус"
                },
                new ListViewItem
                {
                    ImageIndex = 3,
                    Text = "Ограждение"
                },
                new ListViewItem
                {
                    ImageIndex = 4,
                    Text = "Стопка шин"
                },
            };

            structuresListView.Items.AddRange(items.ToArray());
        }

        private void structuresListView_ItemActivate(object sender, EventArgs e)
        {
            engine.ObjectPlacer.SelectObject(structuresListView.SelectedItems[0].Index);
        }
    }
}
