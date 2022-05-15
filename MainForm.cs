using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OpenTK;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Components.Common;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn
{
    struct SecurityStructure
    {
        public string Name;
        public int Price;
    }

    public partial class MainForm : Form
    {
        private readonly RennEngine engine = RennEngine.Instance;
        private readonly GLControl smoothGlControl;
        private AutodromeObjectsSetup autodromeObjectsSetup = new AutodromeObjectsSetup();
        private int drivingCar = -1;

        // game
        private SecurityStructure[] securityStructures;
        private int money = 1000000;
        private int structuresCount;
        private readonly int maxStructureCount = 128;

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

            InitObjectPickerMenu();
            InitSecurityStructures();
            InitStructuresMenu();

            budgetLabel.Text = $@"{money:# ### ##0.00₽;(# ##0.00₽);0}";
            structCountLabel.Text = structuresCount + "/" + maxStructureCount;
        }

        private void InitSecurityStructures()
        {
            securityStructures = new []
            {
                new SecurityStructure
                {
                    Name = "Барьер",
                    Price = 2540
                },
                new SecurityStructure
                {
                    Name = "Буфер",
                    Price = 9700
                },
                new SecurityStructure
                {
                    Name = "Конус",
                    Price = 2970
                },
                new SecurityStructure
                {
                    Name = "Ограждение",
                    Price = 5900
                },
                new SecurityStructure
                {
                    Name = "Стопка шин",
                    Price = 12200
                }
            };
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            engine.Setup(smoothGlControl, this, new HashSet<IObjectsSetup> { autodromeObjectsSetup });
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
            imageList.ImageSize = new Size(42, 42);
            imageList.Images.Add(new Bitmap(@"Assets\UI\barrier-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\buffer-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\conus-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\fence-icon.jpg"));
            imageList.Images.Add(new Bitmap(@"Assets\UI\wheels-icon.jpg"));

            structuresListView.LargeImageList = imageList;

            int i = 0;
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (var securityStructure in securityStructures)
            {
                ListViewItem item = new ListViewItem
                {
                    ImageIndex = i++,
                    Text = $@"{securityStructure.Name} - {securityStructure.Price}₽"
                };
                items.Add(item);
            }

            structuresListView.Items.AddRange(items.ToArray());
        }

        private void InitObjectPickerMenu()
        {
            engine.ObjectPicker.OnObjectPicked += OnObjectSelect;
            engine.ObjectPlacer.OnObjectPlace += OnObjectPlace;
        }

        //
        //
        // EVENTS
        //
        //

        private void OnObjectSelect(RennObject selectedObject)
        {
            if (selectedObject != null)
            {
                sidebarContainer.Visible = true;
                Selectable selectable = selectedObject.GetComponent<Selectable>();
                if(selectable == null) return;
                selectedObjectNameLabel.Text = selectable.Name;

                // transform
                Vector3 position = selectedObject.Transform.position;
                Vector3 rotation = selectedObject.Transform.rotation;
                Vector3 scale = selectedObject.Transform.scale;

                objectXPositionUpDown.Value = (decimal)position.X;
                objectYPositionUpDown.Value = (decimal)position.Y;
                objectZPositionUpDown.Value = (decimal)position.Z;

                objectXRotationTrackBar.Value = (int)rotation.X;
                objectYRotationTrackBar.Value = (int)rotation.Y;
                objectZRotationTrackBar.Value = (int)rotation.Z;
                objectXRotationLabel.Text = objectXRotationTrackBar.Value.ToString(CultureInfo.CurrentCulture);
                objectYRotationLabel.Text = objectYRotationTrackBar.Value.ToString(CultureInfo.CurrentCulture);
                objectZRotationLabel.Text = objectZRotationTrackBar.Value.ToString(CultureInfo.CurrentCulture);

                objectXScaleTrackBar.Value = (int)(scale.X * 10);
                objectYScaleTrackBar.Value = (int)(scale.Y * 10);
                objectZScaleTrackBar.Value = (int)(scale.Z * 10);
                objectXScaleLabel.Text = (objectXScaleTrackBar.Value / 10.0f).ToString(CultureInfo.CurrentCulture);
                objectYScaleLabel.Text = (objectYScaleTrackBar.Value / 10.0f).ToString(CultureInfo.CurrentCulture);
                objectZScaleLabel.Text = (objectZScaleTrackBar.Value / 10.0f).ToString(CultureInfo.CurrentCulture);

                // materials
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(20, 20);
                List<Material> materials = selectable.Materials;
                objectMaterialListView.Items.Clear();
                if (materials == null || materials.Count == 0) return;

                List<ListViewItem> items = new List<ListViewItem>();
                int imageIndex = 0;
                foreach (var material in materials)
                {
                    imageList.Images.Add(new Bitmap(material.Texture.Image));
                    items.Add(new ListViewItem
                    {
                        Text = material.Name,
                        BackColor = material.Color,
                        ImageIndex = imageIndex++
                    });
                }

                objectMaterialListView.SmallImageList = imageList;
                objectMaterialListView.Items.AddRange(items.ToArray());
            }
            else
            {
                sidebarContainer.Visible = false;
            }
        }

        private void structuresListView_ItemActivate(object sender, EventArgs e)
        {
            engine.ObjectPlacer.SelectObject(structuresListView.SelectedItems[0].Index);
        }

        private void objectPositionUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (engine.ObjectPicker.CurrentObject.GetComponent<Selectable>().CanChangeTransform)
            {
                engine.ObjectPicker.CurrentObject.Transform.SetPosition((float)objectXPositionUpDown.Value,
                    (float)objectYPositionUpDown.Value, (float)objectZPositionUpDown.Value);
            }
        }

        private void objectRotationTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (engine.ObjectPicker.CurrentObject.GetComponent<Selectable>().CanChangeTransform)
            {
                engine.ObjectPicker.CurrentObject.Transform.SetRotation(objectXRotationTrackBar.Value,
                    objectYRotationTrackBar.Value, objectZRotationTrackBar.Value);
                objectXRotationLabel.Text = objectXRotationTrackBar.Value.ToString(CultureInfo.CurrentCulture);
                objectYRotationLabel.Text = objectYRotationTrackBar.Value.ToString(CultureInfo.CurrentCulture);
                objectZRotationLabel.Text = objectZRotationTrackBar.Value.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void objectScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            if (engine.ObjectPicker.CurrentObject.GetComponent<Selectable>().CanChangeTransform)
            {
                engine.ObjectPicker.CurrentObject.Transform.SetScale(objectXScaleTrackBar.Value / 10.0f,
                    objectYScaleTrackBar.Value / 10.0f, objectZScaleTrackBar.Value / 10.0f);
                objectXScaleLabel.Text = (objectXScaleTrackBar.Value / 10.0f).ToString(CultureInfo.CurrentCulture);
                objectYScaleLabel.Text = (objectYScaleTrackBar.Value / 10.0f).ToString(CultureInfo.CurrentCulture);
                objectZScaleLabel.Text = (objectZScaleTrackBar.Value / 10.0f).ToString(CultureInfo.CurrentCulture);
            }
        }

        private void objectMaterialListView_ItemActivate(object sender, EventArgs e)
        {
            MeshRenderer renderer = engine.ObjectPicker.CurrentObject.GetComponent<MeshRenderer>();
            Selectable selectable = engine.ObjectPicker.CurrentObject.GetComponent<Selectable>();
            List<Material> materials = selectable.Materials;
            
            int index = objectMaterialListView.SelectedItems[0].Index;
            renderer.SetTexture(materials[index].Texture);
            renderer.SetShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(materials[index].Color));
            Color color = materials[index].Color;
            selectable.DefaultColor = new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1f);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            DriveCar(0);
            ChangePickerState(false);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ChangePickerState(false);
                DriveCar(-1);
            }
        }

        private void changeCarButton_Click(object sender, EventArgs e)
        {
            smoothGlControl.Focus();
            int index = drivingCar == autodromeObjectsSetup.CarControllers.Length - 1 ? 0 : drivingCar + 1;
            DriveCar(index);
        }

        private void DriveCar(int index)
        {
            if (index == -1)
            {
                Camera.Instance.Controller.Target = null;
                if(drivingCar != -1) autodromeObjectsSetup.CarControllers[drivingCar].IsInputReg = false;
                drivingCar = index;
                ChangePickerState(true);
                changeCarButton.Visible = false;
            }
            else
            {
                if (drivingCar != -1)
                {
                    Camera.Instance.Controller.Target = null;
                    autodromeObjectsSetup.CarControllers[drivingCar].IsInputReg = false;
                    drivingCar = index;
                }

                drivingCar = index;
                Camera.Instance.Controller.Target = autodromeObjectsSetup.CarCameraTargetTransforms[drivingCar];
                autodromeObjectsSetup.CarControllers[drivingCar].IsInputReg = true;
                changeCarButton.Visible = true;
            }
            
        }

        private void ChangePickerState(bool canPick)
        {
            engine.ObjectPicker.CanPick = canPick;
            engine.ObjectPicker.CurrentObject = null;
            sidebarContainer.Visible = false;
        }

        private void OnObjectPlace(int index)
        {
            BuyStructure(securityStructures[index].Price);
        }

        private void BuyStructure(int price)
        {
            money -= price;
            budgetLabel.Text = $@"{money:# ##0.00₽;(# ##0.00₽);0}";
            structuresCount++;
            structCountLabel.Text = structuresCount + @"/" + maxStructureCount;
        }
    }
}
