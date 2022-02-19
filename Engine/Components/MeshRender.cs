using OpenTK;
using OpenTK.Graphics.ES11;
using System;

namespace TwoBRenn.Engine.Components
{
    class MeshRender : Component
    {
        private Action renderAction;

        public override void OnUpdate()
        {
            GL.PushMatrix();
            GL.Translate(rennObject.globalPosition.X, rennObject.globalPosition.Y, rennObject.globalPosition.Z);
            GL.Scale(rennObject.globalScale.X, rennObject.globalScale.Y, rennObject.globalScale.Z);
            GL.Rotate(MathHelper.DegreesToRadians(rennObject.globalRotation.X), 1.0f, 0.0f, 0.0f);
            GL.Rotate(MathHelper.DegreesToRadians(rennObject.globalRotation.Y), 0.0f, 1.0f, 0.0f);
            GL.Rotate(MathHelper.DegreesToRadians(rennObject.globalRotation.Z), 0.0f, 0.0f, 1.0f);

            renderAction?.Invoke();
            GL.PopMatrix();
        }

        public void SetRenderAction(Action action)
        {
            renderAction = action;
        }
    }
}
