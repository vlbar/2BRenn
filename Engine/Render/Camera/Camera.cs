using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Common.RayCasting;
using TwoBRenn.Engine.Interfaces;

namespace TwoBRenn.Engine.Render.Camera
{
    class Camera : IUpdatableEnginePart
    {
        private static Camera _instance;
        public static Camera Instance => _instance ?? (_instance = new Camera());

        public CameraController Controller { get; set; }
        private int width;
        private int height;
        private bool isOrthographic;
        private float fov = 75;
        private readonly float clipingNear = 0.1f;
        private readonly float clipingFar = 500f;

        private static Matrix4 _projection;
        private static Matrix4 _view;

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

            _view = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
        }

        public void SetupProjection()
        {
            if (isOrthographic)
            {
                _projection = Matrix4.CreateOrthographic(5, 5, clipingNear, clipingFar);
            }
            else
            {
                float aspectRatio = width / (float)height;
                _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectRatio, clipingNear, clipingFar);
            }
        }

        public void OnUpdate()
        {
            Controller.OnUpdate();
        }

        public static Matrix4 GetViewMatrix() => _view;
        public static Vector3 Position => _view.Inverted().ExtractTranslation();

        public static bool IsOrthographic
        {
            get => Instance.isOrthographic;
            set => Instance.isOrthographic = value;
        }

        public static Matrix4 GetProjectionMatrix() => _projection;
        public void SetViewMatrix(Matrix4 view) => _view = view;

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
