﻿using System;
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
        public ObjectPlacer ObjectPlacer = new ObjectPlacer();
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

            sceneManager = new SceneManager();
            ObjectPlacer.SceneManager = sceneManager;
            RenderControl.SetupGlControl(glControl);

            InputManager.GlControl = GlControl;
            InputManager.Form = Form;

            preciseTimer = new Stopwatch();
            Application.Idle += Application_Idle;
            GlControl.Paint += GlControl_Paint;
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
            camera.OnUpdate();
            ObjectPlacer.OnUpdate();
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

        private void Application_Idle(object sender, EventArgs e)
        {
            while (GlControl.IsIdle)
            {
                UpdateCycle();
            }
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            UpdateCycle();
        }
    }
}
