using OpenTK;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components
{
    class LoopRotation : Component
    {
        public float Speed = 1;
        public Vector3 AxisMultiplier = Vector3.UnitZ;
        private Vector3 initialRotation;
        private float rotation;

        public override void OnStart()
        {
            initialRotation = rennObject.Transform.rotation;
        }

        public override void OnUpdate()
        {
            rotation += Speed * Time.DeltaTime;
            rennObject.Transform.SetRotation(initialRotation.X + rotation * AxisMultiplier.X,
                initialRotation.Y + rotation * AxisMultiplier.Y, initialRotation.Z + rotation * AxisMultiplier.Z);
        }
    }
}