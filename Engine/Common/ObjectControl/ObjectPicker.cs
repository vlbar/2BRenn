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
            if (Input.IsMouseButtonDown(MouseButtons.Left))
            {
                if (Physics.Raycast(Camera.ScreenPointToRay(Input.MouseRelativePosition), out var hit))
                {
                    CurrentObject = hit.HitObject;
                }
            }

            if (Input.IsMouseButtonDown(MouseButtons.Right))
            {
                CurrentObject = null;
            }
        }
    }
}
