using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Common.RayCasting;

namespace TwoBRenn.Engine.Render.Camera
{
    class Camera
    {
        private static Camera instance;
        public CameraController Controller { get; set; }
        private int width;
        private int height;
        private bool isOrthographic = false;
        private float fov = 75;
        private float clipingNear = 0.1f;
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

            view = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
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

        public static Ray ScreenPointToRay(Vector2 screenPosition)
        {
            float width = RennEngine.Instance.GlControl.Width;
            float height = RennEngine.Instance.GlControl.Height;

            // viewport coordinate system
            Vector2 size = new Vector2(width, height);

            // normalized device coordinates
            var x = 2f * screenPosition.X / size.X - 1f;
            var y = 1f - 2f * screenPosition.Y / size.Y;
            var z = 1f;
            var rayNormalizedDeviceCoordinates = new Vector3(x, y, z);

            // 4D homogeneous clip coordinates
            var rayClip = new Vector4(rayNormalizedDeviceCoordinates.X, rayNormalizedDeviceCoordinates.Y, -1f, 1f);

            // 4D eye (camera) coordinates
            var rayEye = GetProjectionMatrix().Inverted() * rayClip;
            rayEye = new Vector4(rayEye.X, rayEye.Y, -1f, 0f);

            // 4D world coordinates
            var rayWorldCoordinates = (GetViewMatrix() * rayEye).Xyz;
            rayWorldCoordinates.Normalize();

            return new Ray
            {
                Origin = GetViewMatrix().Inverted().ExtractTranslation(),
                Direction = rayWorldCoordinates
            };
        }
    }
}
