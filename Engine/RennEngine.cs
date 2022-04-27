using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Common.ObjectControl;
using TwoBRenn.Engine.Render;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.Utils;
using TwoBRenn.Engine.Scene;
using Timer = System.Windows.Forms.Timer;

namespace TwoBRenn.Engine
{
    class RennEngine
    {
        private static RennEngine _instance;
        public static RennEngine Instance => _instance ?? (_instance = new RennEngine());

        // forms
        public Form Form { private set; get; }
        public GLControl GlControl { private set; get; }

        // parts
        private Camera camera;
        private SceneManager sceneManager;
        private Input inputManager;
        private Physics physicsManager;
        public DebugManager DebugManager;
        public ObjectPlacer ObjectPlacer = new ObjectPlacer();
        public ObjectPicker ObjectPicker = new ObjectPicker();
        public RenderControl RenderControl { get; } = new RenderControl();

        // cycle time
        private Time time;
        private int maxFrameRate = 75;
        private Stopwatch preciseTimer;
        private double accumulator;
        private int frameCount;
        public int Fps;

        public RennEngine()
        {
            time = Time.GetInstance();
        }

        public void Setup(GLControl glControl, Form form)
        {
            GlControl = glControl;
            Form = form;
            camera = Camera.GetInstance();
            inputManager = Input.Instance;
            physicsManager = Physics.Instance;
            DebugManager = DebugManager.Instance;

            sceneManager = new SceneManager();
            ObjectPlacer.SceneManager = sceneManager;
            RenderControl.SetupGlControl(glControl);
            inputManager.Setup(glControl, form);

            preciseTimer = new Stopwatch();
            Timer renderTimer = new Timer();
            renderTimer.Interval = 1000 / maxFrameRate;
            renderTimer.Tick += delegate { UpdateCycle(); };
            renderTimer.Start();
        }

        private void UpdateCycle()
        {
            UpdateOtherParts();
            RenderControl.PreRender();
            sceneManager.OnUpdate();
            RenderControl.PostRender();
            sceneManager.OnLateUpdate();
            RenderControl.EndRender();
            sceneManager.OnLateUpdate();

            // frames
            float frameTime = (float)GetElapsedTime();
            float waitingTime = Math.Max(0, (1000.0f / maxFrameRate) - frameTime);
            if (waitingTime > 0) Thread.Sleep((int)waitingTime);
            Accumulate(frameTime + waitingTime);
            time.CaptureFrametime(frameTime + waitingTime);
        }

        private void UpdateOtherParts()
        {
            inputManager.OnUpdate();
            camera.OnUpdate();
            ObjectPlacer.OnUpdate();
            ObjectPicker.OnUpdate();
            physicsManager.OnUpdate();
        }

        private double GetElapsedTime()
        {
            preciseTimer.Stop();
            double elapsedTime = preciseTimer.Elapsed.TotalMilliseconds;
            preciseTimer.Reset();
            preciseTimer.Start();

            return elapsedTime;
        }

        private void Accumulate(float milliseconds)
        {
            frameCount++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                Fps = frameCount;
                accumulator -= 1000;
                frameCount = 0;
            }
        }
    }
}
