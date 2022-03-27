namespace TwoBRenn.Engine.Render.Camera
{
    abstract class CameraController
    {
        public Camera camera { get; set; }

        public CameraController(Camera camera)
        {
            this.camera = camera;
        }
        public abstract void OnUpdate();
    }
}
