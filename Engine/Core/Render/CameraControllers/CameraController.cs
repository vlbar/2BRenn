using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoBRenn.Engine.Core.Render.CameraControllers
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
