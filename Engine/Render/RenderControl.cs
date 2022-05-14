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

        private int controlWidth;
        private int controlHeight;

        public RenderControl()
        {
            camera = Camera.Camera.Instance;
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
            controlWidth = glControl.Width;
            controlHeight = glControl.Height;

            camera.SetupViewport(controlWidth, controlHeight);
            glControl.Invalidate();
        }

        public void PreRender()
        {
            glControl.MakeCurrent();
            GL.Viewport(0, 0, controlWidth, controlHeight);
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
