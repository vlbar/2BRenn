using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components
{
    class Rigidbody : Component
    {
        public Vector3 Force;
        public Vector3 Velocity;
        public float Mass = 1;
        public ICollider Collider;

        public override void OnStart()
        {
            Physics.AddRigidbody(this);
            Collider = rennObject.GetComponent<BoxCollider>();
            Collider.IsDynamic = true;
        }

        public void AddForce(Vector3 vector)
        {
            Force += vector / Mass;
        }

        public override void OnUpdate()
        {
            rennObject.Transform.Translate(Velocity + Force);
            Force = Stabilize(Force);
            Velocity = Vector3.Zero;
        }

        private Vector3 Stabilize(Vector3 value, float speed = 1, float target = 0)
        {
            float x = Stabilize(value.X, speed, target);
            float y = Stabilize(value.Y, speed, target);
            float z = Stabilize(value.Z, speed, target);

            return new Vector3(x, y, z);
        }

        private float Stabilize(float value, float speed, float target = 0)
        {
            if (value > target)
            {
                value -= speed * Time.DeltaTime;
                if (value < target) value = target;
            }

            if (value < target)
            {
                value += speed * Time.DeltaTime;
                if (value > target) value = target;
            }

            return value;
        }
    }
}
