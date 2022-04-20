using System.Windows.Forms;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Camera;

namespace TwoBRenn.Engine.Common.ObjectControl
{
    class ObjectPicker
    {
        public RennObject CurrentObject;

        public void OnUpdate()
        {
            if (InputManager.IsMouseButtonDown(MouseButtons.Left))
            {
                if (PhysicsManager.Raycast(Camera.ScreenPointToRay(InputManager.MouseRelativePosition), out var hit))
                {
                    CurrentObject = hit.HitObject;
                }
            }

            if (InputManager.IsMouseButtonDown(MouseButtons.Right))
            {
                CurrentObject = null;
            }
        }
    }
}
