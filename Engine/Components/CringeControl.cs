using OpenTK;
using OpenTK.Input;

namespace TwoBRenn.Engine.Components
{
    class CringeControl : Component
    {
        private Rigidbody rigidbody;
        private float rotation;

        public override void OnStart()
        {
            rigidbody = rennObject.GetComponent<Rigidbody>();
        }

        public override void OnUpdate()
        {
            KeyboardState input = Keyboard.GetState();

            float speed = 0.8f;
            if (input.IsKeyDown(Key.I))
            {
                rigidbody.AddForce(-Vector3.UnitZ * speed);
            }

            if (input.IsKeyDown(Key.K))
            {
                rigidbody.AddForce(Vector3.UnitZ * speed);
            }

            if (input.IsKeyDown(Key.J))
            {
                rigidbody.AddForce(-Vector3.UnitX * speed);
            }

            if (input.IsKeyDown(Key.L))
            {
                rigidbody.AddForce(Vector3.UnitX * speed);
            }

            if (input.IsKeyDown(Key.U))
            {
                rotation += 2;
                rennObject.Transform.SetRotation(0, rotation, 0);
            }
        }
    }
}
