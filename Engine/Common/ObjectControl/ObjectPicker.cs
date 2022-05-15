using System;
using System.Windows.Forms;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Camera;

namespace TwoBRenn.Engine.Common.ObjectControl
{
    class ObjectPicker
    {
        public bool CanPick = true;
        public RennObject CurrentObject;
        public Action<RennObject> OnObjectPicked;

        public void OnUpdate()
        {
            if (!CanPick) return;
            if (Input.IsMouseButtonDown(MouseButtons.Left))
            {
                if (Physics.Raycast(Camera.ScreenPointToRay(Input.MouseRelativePosition), out var hit))
                {
                    CurrentObject = hit.HitObject;
                    OnObjectPicked?.Invoke(CurrentObject);
                }
            }

            if (Input.IsMouseButtonDown(MouseButtons.Right))
            {
                CurrentObject = null;
                OnObjectPicked?.Invoke(null);
            }
        }
    }
}
