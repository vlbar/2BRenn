using OpenTK;
using OpenTK.Input;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Render.Camera
{
    class OrbitCameraController : CameraController
    {
        public float MovementSpeed = 8.5f;
        public float RotationSensitivity = 0.2f;
        public float ZoomSensitivity = 15f;

        public Vector2 PitchLimit = new Vector2(-10, 90);
        public Vector2 ZoomLimit = new Vector2(2, 15);

        private Vector3 targetPosition;
        private float yaw;
        private float pitch = 45f;
        private float zoomAmount = 5f;

        private Vector3 forward = Vector3.UnitZ;
        private Vector3 up = Vector3.UnitY;
        private Vector3 right = Vector3.UnitX;

        private bool isFirstMouseMove = true;
        private Vector2 lastMousePosition;
        private float lastMouseWheelPrecise = 0;

        public OrbitCameraController(Camera camera) : base(camera) { }

        private Matrix4 GetViewMatrix()
        {
            Matrix4 targetTranslateMatrix = Matrix4.CreateTranslation(targetPosition);
            Matrix4 rotationX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            Matrix4 rotationY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            Matrix4 targetModelMatrix = targetTranslateMatrix * rotationY * rotationX;

            Matrix4 cameraTranslateMatrix = Matrix4.CreateTranslation(new Vector3(0, 0, -zoomAmount));
            Matrix4 translateMatrix = targetModelMatrix * cameraTranslateMatrix;
            return translateMatrix;
        }

        private void Move()
        {
            KeyboardState input = Keyboard.GetState();

            if (Target == null)
            {
                if (input.IsKeyDown(Key.W))
                {
                    targetPosition += forward * MovementSpeed * Time.DeltaTime;
                }

                if (input.IsKeyDown(Key.S))
                {
                    targetPosition -= forward * MovementSpeed * Time.DeltaTime;
                }

                if (input.IsKeyDown(Key.A))
                {
                    targetPosition += right * MovementSpeed * Time.DeltaTime;
                }

                if (input.IsKeyDown(Key.D))
                {
                    targetPosition += -right * MovementSpeed * Time.DeltaTime;
                }
            }
            else
            {
                targetPosition = -Target.GetGlobalModelMatrix().ExtractTranslation();
            }
        }

        private void Rotate()
        {
            MouseState mouse = Mouse.GetState();

            bool isDragMode = mouse.IsButtonDown(MouseButton.Middle) || Target != null;
            if (isDragMode)
            {
                if (isFirstMouseMove)
                {
                    lastMousePosition = new Vector2(mouse.X, mouse.Y);
                    isFirstMouseMove = false;
                }
                else
                {
                    var deltaX = mouse.X - lastMousePosition.X;
                    var deltaY = mouse.Y - lastMousePosition.Y;
                    lastMousePosition = new Vector2(mouse.X, mouse.Y);

                    yaw += deltaX * RotationSensitivity;
                    pitch += deltaY * RotationSensitivity;
                    pitch = MathHelper.Clamp(pitch + deltaY * RotationSensitivity, PitchLimit.X, PitchLimit.Y);
                }
                UpdateVectors();
            }
            else
            {
                isFirstMouseMove = true;
            }
        }

        private void UpdateVectors()
        {
            forward = new Quaternion(0, MathHelper.DegreesToRadians(-yaw), 0) * Vector3.UnitZ;
            right = -Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, forward));
        }

        private void Zoom()
        {
            MouseState mouse = Mouse.GetState();
            float wheelPrecise = mouse.WheelPrecise;
            if (wheelPrecise != lastMouseWheelPrecise)
            {
                if (wheelPrecise > lastMouseWheelPrecise)
                {
                    zoomAmount -= ZoomSensitivity * Time.DeltaTime;
                }
                else if (wheelPrecise < lastMouseWheelPrecise)
                {
                    zoomAmount += ZoomSensitivity * Time.DeltaTime;
                }

                zoomAmount = MathHelper.Clamp(zoomAmount, ZoomLimit.X, ZoomLimit.Y);
                lastMouseWheelPrecise = wheelPrecise;
            }
        }

        public override void OnUpdate()
        {
            Move();
            Rotate();
            Zoom();
            camera.SetViewMatrix(GetViewMatrix());
        }

        public override void SetPosition(Vector3 vector)
        {
            targetPosition = vector;
        }

        public override void SetRotation(Vector3 vector)
        {
            pitch = vector.X;
            yaw = vector.Y;
            zoomAmount = vector.Z;
        }
    }
}
