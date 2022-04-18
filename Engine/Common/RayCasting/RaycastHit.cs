using OpenTK;

namespace TwoBRenn.Engine.Common.RayCasting
{
    struct RaycastHit
    {
        public RennObject HitObject;
        public Vector3 Origin;
        public Vector3? Point;
        public Vector3 Normal;

        public float Distance
        {
            get
            {
                if (Point == null) return 0;
                return Vector3.Distance(Origin, (Vector3)Point);
            }
        } 
    }
}
