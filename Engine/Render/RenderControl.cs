using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Render.Textures;

namespace TwoBRenn.Engine.Render
{
    class RenderControl
    {
        private GLControl glControl;
        private Camera.Camera camera;
        public Skybox Skybox { get; set; }

        public Action OnSetup { get; set; }

        public int CurrentShaderProgram = -1;

        public RenderControl()
        {
            camera = Camera.Camera.GetInstance();
        }

        public void SetupGlControl(GLControl glControl)
        {
            this.glControl = glControl;
            glControl.VSync = true;

            SetupViewport();
            glControl.Resize += delegate
            {
                SetupViewport();
            };

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

        public void PreRender()
        {
            glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.LightBlue);
        }

        public void PostRender()
        {
            if (Skybox != null) Skybox.Use();
        }

        public void EndRender()
        {
            glControl.SwapBuffers();
        }
    }
}
