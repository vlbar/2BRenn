using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TwoBRenn.Engine.Core.Render
{
    class RenderControl
    {
        private GLControl glControl;

        public Action OnSetup { get; set; }
        public Action OnRender { get; set; }
        
        private int maxFrameRate = 75;
        private Stopwatch preciseTimer;
        private double accumulator = 0;
        private int frameCount = 0;
        private int fps = 0;

        public void SetupGlControl(GLControl glControl)
        {
            glControl.VSync = false;
            this.glControl = glControl;

            SetupViewport();
            glControl.Resize += delegate
            {
                SetupViewport();
            };

            preciseTimer = new Stopwatch();
            Application.Idle += Application_Idle;
            glControl.Paint += GlControl_Paint;

            OnSetup?.Invoke();
        }

        private void SetupViewport()
        {
            int width = glControl.Width;
            int height = glControl.Height;

            GL.Viewport(0, 0, width, height);
            glControl.Invalidate();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            while (glControl.IsIdle)
            {
                Render();
            }
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.LightBlue);

            // frames
            float frameTime = (float)GetElapsedTime();
            float waitingTime = Math.Max(0, (1000.0f / maxFrameRate) - frameTime);
            if (waitingTime > 0) Thread.Sleep((int)waitingTime);
            Accumulate(frameTime + waitingTime);

            // render cycle
            OnRender?.Invoke();

            glControl.SwapBuffers();
        }

        private double GetElapsedTime()
        {
            preciseTimer.Stop();
            double deltaTime = preciseTimer.Elapsed.TotalMilliseconds;
            preciseTimer.Reset();
            preciseTimer.Start();

            return deltaTime;
        }

        private void Accumulate(float milliseconds)
        {
            frameCount++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                fps = frameCount;
                accumulator -= 1000;
                frameCount = 0;
            }
        }

        public string GetDynamicDebugInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{fps} FPS");

            return stringBuilder.ToString();
        }

        public string GetStaticDebugInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GL.GetString(StringName.Vendor)).Append(" ");
            stringBuilder.Append(GL.GetString(StringName.Renderer)).Append(" ");
            stringBuilder.Append("(").Append(GL.GetString(StringName.ShadingLanguageVersion)).AppendLine(")");

            return stringBuilder.ToString();
        }
    }
}
