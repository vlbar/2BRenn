using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Core.Render.CameraControllers;

namespace TwoBRenn.Engine.Core.Render
{
    class Camera
    {
        private static Camera instance;
        public CameraController Controller { get; set; }
        private int width;
        private int height;
        private bool isOrthographic = false;
        private float fov = 90;
        private float clipingNear = 0.01f;
        private float clipingFar = 100f;

        private static Matrix4 projection;
        private static Matrix4 view;

        private Camera()
        {
            Controller = new OrbitCameraController(this);
        }

        public void SetupViewport(int width, int height)
        {
            this.width = width;
            this.height = height;
            GL.Viewport(0, 0, width, height);
            SetupProjection();

            view = Matrix4.LookAt(Vector3.Zero, Vector3.UnitX, Vector3.UnitZ);
        }

        public void SetupProjection()
        {
            if (isOrthographic)
            {
                projection = Matrix4.CreateOrthographic(5, 5, clipingNear, clipingFar);
            }
            else
            {
                float aspectRatio = width / (float)height;
                projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectRatio, clipingNear, clipingFar);
            }
        }

        public static Camera GetInstance()
        {
            if (instance == null)
                instance = new Camera();
            return instance;
        }

        public void OnUpdate()
        {
            Controller.OnUpdate();
        }

        public static Matrix4 GetViewMatrix() => view;
        public static Matrix4 GetProjectionMatrix() => projection;
        public void SetViewMatrix(Matrix4 _view) => view = _view;
    }
}
