using System;
using OpenTK;

namespace TwoBRenn.Engine.Common.RayCasting
{
    static class Raycast
    {
        public static RaycastHit IntersectionWithPlane(Ray ray, Vector4 plane)
        {
            RaycastHit hit = new RaycastHit
            {
                Origin = ray.Origin
            };
            float denominator = plane.X * ray.Direction.X + plane.Y * ray.Direction.Y + plane.Z * ray.Direction.Z;
            if (denominator == 0) return hit;

            float numerator = plane.W + plane.X * ray.Origin.X + plane.Y * ray.Origin.Y + plane.Z * ray.Origin.Z;
            if (denominator == 0 && numerator == 0) return hit;

            float t = -(numerator / denominator);

            hit.Point = new Vector3(
                ray.Origin.X + ray.Direction.X * t,
                ray.Origin.Y + ray.Direction.Y * t,
                ray.Origin.Z + ray.Direction.Z * t
            );
            hit.Normal = plane.Xyz;
            return hit;
        }

        public static RaycastHit IntersectionWithBox(Ray ray, Vector3 minBound, Vector3 maxBound)
        {
            RaycastHit hit = new RaycastHit
            {
                Origin = ray.Origin
            };
            Vector3 directionFraction;

            directionFraction.X = 1.0f / ray.Direction.X;
            directionFraction.Y = 1.0f / ray.Direction.Y;
            directionFraction.Z = 1.0f / ray.Direction.Z;

            float t1 = (minBound.X - ray.Origin.X) * directionFraction.X;
            float t2 = (maxBound.X - ray.Origin.X) * directionFraction.X;
            float t3 = (minBound.Y - ray.Origin.Y) * directionFraction.Y;
            float t4 = (maxBound.Y - ray.Origin.Y) * directionFraction.Y;
            float t5 = (minBound.Z - ray.Origin.Z) * directionFraction.Z;
            float t6 = (maxBound.Z - ray.Origin.Z) * directionFraction.Z;

            float tMin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            float tMax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            // if tmax < 0, ray is intersecting AABB, but the whole AABB is behind us
            // if tmin > tmax, ray doesn't intersect AABB
            if (tMax < 0 || tMin > tMax)
            {
                return hit;
            }

            hit.Point = new Vector3(
                (minBound.X + maxBound.X) / 2.0f,
                (minBound.Y + maxBound.Y) / 2.0f,
                (minBound.Z + maxBound.Z) / 2.0f);
            return hit;
        }
    }
}
