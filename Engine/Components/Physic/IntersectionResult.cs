using OpenTK;

namespace TwoBRenn.Engine.Components.Physic
{
    struct IntersectionResult
    {
        public ICollider ColliderA;
        public ICollider ColliderB;
        public Vector3 Normal;
        public Vector3 Force;
    }
}
