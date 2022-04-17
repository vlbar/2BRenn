using OpenTK;

namespace TwoBRenn.Engine.Common.RayCasting
{
    static class Raycast
    {
        public static Vector3? IntersectionWithPlane(Vector3 origin, Vector3 direction, Vector4 plane)
        {
            float denominator = plane.X * direction.X + plane.Y * direction.Y + plane.Z * direction.Z;
            if (denominator == 0) return null;

            float numerator = plane.W + plane.X * origin.X + plane.Y * origin.Y + plane.Z * origin.Z;
            if (denominator == 0 && numerator == 0) return null;

            float t = -(numerator / denominator);

            return new Vector3(
                origin.X + direction.X * t,
                origin.Y + direction.Y * t,
                origin.Z + direction.Z * t
            );
        }
    }
}
