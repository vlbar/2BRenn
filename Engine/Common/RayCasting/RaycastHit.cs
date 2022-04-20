using OpenTK;

namespace TwoBRenn.Engine.Common.RayCasting
{
    struct RaycastHit
    {
        public RennObject HitObject;
        public Vector3 Origin;
        public Vector3? Point;
        public Vector3 Normal;
        private float? distance;

        public float Distance
        {
            get
            {
                if (Point == null)
                {
                    return 0;
                }
                if (distance == null)
                {
                    distance = Vector3.Distance(Origin, (Vector3)Point);
                }
                return (float)distance;
            }
            set => distance = value;
        } 
    }
}
