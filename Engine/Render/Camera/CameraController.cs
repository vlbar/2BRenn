using OpenTK;
using TwoBRenn.Engine.Utils;

namespace TwoBRenn.Engine.Render.Camera
{
    abstract class CameraController
    {
        public Camera camera { get; set; }
        public Transform Target;

        public CameraController(Camera camera)
        {
            this.camera = camera;
        }
        public abstract void OnUpdate();
    }
}
