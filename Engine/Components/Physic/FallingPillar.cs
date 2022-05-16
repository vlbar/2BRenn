using OpenTK;

namespace TwoBRenn.Engine.Components.Physic
{
    class FallingPillar : Component
    {
        public float Strength = 5f;
        public Vector3 BlockAxis = Vector3.One;
        public float? FallenY = null;
        private BoxCollider collider;
        private Vector3 rotation = new Vector3(0, 0, 0);
        private Vector3 currentRotation;
        private Vector3 initialRotation;
        private float yRotation;

        public override void OnStart()
        {
            initialRotation = rennObject.Transform.rotation;
            collider = rennObject.GetComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;
            yRotation = initialRotation.Y;
        }

        private void OnCollisionEnter(IntersectionResult intersectionResult)
        {
            if (rotation == Vector3.Zero)
            {
                if (intersectionResult.Force == Vector3.Zero) return;
                intersectionResult.Force *= BlockAxis;
                Vector3 rotationVector = intersectionResult.Force.Normalized();
                rotation = new Vector3(rotationVector.Z, 0, -rotationVector.X);
                if (!collider.IsTrigger) collider.IsTrigger = true;
            }
        }

        public override void OnUpdate()
        {
            currentRotation += rotation * Strength;
            if (currentRotation.X > -90 && currentRotation.X < 90 && currentRotation.Z > -90 && currentRotation.Z < 90)
            {
                currentRotation.X = MathHelper.Clamp(currentRotation.X, -90, 90);
                currentRotation.Z = MathHelper.Clamp(currentRotation.Z, -90, 90);
                rennObject.Transform.SetRotation(currentRotation.X, yRotation, currentRotation.Z);
            }

            if (FallenY != null && rotation != Vector3.Zero)
            {
                if (yRotation < -FallenY && yRotation < 0)
                {
                    yRotation += Strength;
                } 
                else if (yRotation > FallenY && yRotation > 0)
                {
                    yRotation -= Strength;
                }
            }
        }
	}
}
