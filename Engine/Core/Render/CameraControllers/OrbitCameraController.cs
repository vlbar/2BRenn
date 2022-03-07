using OpenTK;
using OpenTK.Input;

namespace TwoBRenn.Engine.Core.Render.CameraControllers
{
    class OrbitCameraController : CameraController
    {
        private float movementSpeed = 1.5f;
        private float rotationSensitivity = 0.5f;
        private float zoomSensitivity = 15f;

        private Vector2 pitchLimit = new Vector2(0, 90);
        private Vector2 zoomLimit = new Vector2(2, 10);

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

            if (input.IsKeyDown(Key.W))
            {
                targetPosition += forward * movementSpeed * Time.deltaTime;
            }

            if (input.IsKeyDown(Key.S))
            {
                targetPosition -= forward * movementSpeed * Time.deltaTime;
            }

            if (input.IsKeyDown(Key.A))
            {
                targetPosition += right * movementSpeed * Time.deltaTime;
            }

            if (input.IsKeyDown(Key.D))
            {
                targetPosition += -right * movementSpeed * Time.deltaTime;
            }
        }

        private void Rotate()
        {
            MouseState mouse = Mouse.GetState();

            bool isDragMode = mouse.IsButtonDown(MouseButton.Middle);
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

                    yaw += deltaX * rotationSensitivity;
                    pitch += deltaY * rotationSensitivity;
                    pitch = MathHelper.Clamp(pitch + deltaY * rotationSensitivity, pitchLimit.X, pitchLimit.Y);
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
                    zoomAmount -= zoomSensitivity * Time.deltaTime;
                }
                else if (wheelPrecise < lastMouseWheelPrecise)
                {
                    zoomAmount += zoomSensitivity * Time.deltaTime;
                }

                zoomAmount = MathHelper.Clamp(zoomAmount, zoomLimit.X, zoomLimit.Y);
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
    }
}
