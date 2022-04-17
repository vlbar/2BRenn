using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Render
{
    class RenderControl
    {
        private GLControl glControl;
        private Camera.Camera camera;
        public Skybox Skybox { get; set; }

        public Action OnSetup { get; set; }
        public Action OnRender { get; set; }
        public Action OnRenderTransparent { get; set; }

        private Time time;
        private int maxFrameRate = 75;
        private Stopwatch preciseTimer;
        private double accumulator = 0;
        private int frameCount = 0;
        private int fps = 0;

        public RenderControl()
        {
            camera = Camera.Camera.GetInstance();
            time = Time.GetInstance();
        }

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

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            OnSetup?.Invoke();
        }

        private void SetupViewport()
        {
            int width = glControl.Width;
            int height = glControl.Height;

            camera.SetupViewport(width, height);
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
            camera.OnUpdate();

            glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.LightBlue);

            // frames
            float frameTime = (float)GetElapsedTime();
            float waitingTime = Math.Max(0, (1000.0f / maxFrameRate) - frameTime);
            if (waitingTime > 0) Thread.Sleep((int)waitingTime);
            Accumulate(frameTime + waitingTime);
            time.CaptureFrametime(frameTime + waitingTime);

            // render cycle
            OnRender?.Invoke();
            if (Skybox != null) Skybox.Use();

            GL.Enable(EnableCap.Blend);
            OnRenderTransparent?.Invoke();

            glControl.SwapBuffers();
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
                fps = frameCount;
                accumulator -= 1000;
                frameCount = 0;
            }
        }

        public string GetDynamicDebugInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{fps} FPS");

            Quaternion q = Camera.Camera.GetViewMatrix().Inverted().ExtractRotation();
            stringBuilder.AppendLine($"XYZL {MathHelper.RadiansToDegrees(q.X):0.0} {MathHelper.RadiansToDegrees(q.Y):0.0} {MathHelper.RadiansToDegrees(q.Z):0.0}");

            MouseState mouse = Mouse.GetState();
            stringBuilder.AppendLine($"MOUSE {mouse.X} {mouse.Y}");

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
