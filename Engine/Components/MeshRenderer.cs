using OpenTK;
using OpenTK.Graphics.ES11;
using System;

namespace TwoBRenn.Engine.Components
{
    class MeshRenderer : Component
    {
        private Action renderAction;

        public override void OnUpdate()
        {
            GL.PushMatrix();
            Matrix4 modelMatrix = rennObject.Transform.GetGlobalModelMatrix();

            // dont hate please, its temporary solution :/
            Vector3 position = modelMatrix.ExtractTranslation();
            Vector3 rotation = modelMatrix.ExtractRotation().Xyz;
            Vector3 scale = modelMatrix.ExtractScale();

            GL.Translate(position.X, position.Y, position.Z);
            GL.Rotate(rotation.X, 1.0f, 0.0f, 0.0f);
            GL.Rotate(rotation.Y, 0.0f, 1.0f, 0.0f);
            GL.Rotate(rotation.Z, 0.0f, 0.0f, 1.0f);

            GL.Scale(scale.X, scale.Y, scale.Z);

            renderAction?.Invoke();
            GL.PopMatrix();
        }

        public void SetRenderAction(Action action)
        {
            renderAction = action;
        }
    }
}
