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

        public static RaycastHit IntersectionWithBox(Ray ray, Vector3 minBound, Vector3 maxBound, Matrix4 modelMatrix)
        {
            RaycastHit hit = new RaycastHit
            {
                Origin = ray.Origin
            };

            float tMin = 0;
            float tMax = 100000;
            float threshold = 0.001f;

            Vector3 position = modelMatrix.ExtractTranslation();
            Vector3 scale = modelMatrix.ExtractScale();
            Vector3 delta = position - ray.Origin;

            modelMatrix = modelMatrix.ClearScale();

            float e, f;

            // Test intersection with the 2 planes perpendicular to the OBB's X axis
            Vector3 xaxis = new Vector3(modelMatrix.Column0.X, modelMatrix.Column0.Y, modelMatrix.Column0.Z);
            e = VectorsDot(xaxis, delta);
            f = VectorsDot(ray.Direction, xaxis);

            if (Math.Abs(f) > threshold)
            {
                float t1 = (e + minBound.X * scale.X) / f;
                float t2 = (e + maxBound.X * scale.X) / f;

                if (t1 > t2)
                {
                    MathHelper.Swap(ref t1, ref t2);
                }

                // tMax is the nearest "far" intersection (amongst the X,Y and Z planes pairs)
                if (t2 < tMax)
                {
                    tMax = t2;
                }
                // tMin is the farthest "near" intersection (amongst the X,Y and Z planes pairs)
                if (t1 > tMin)
                {
                    tMin = t1;
                }

                if (tMax < tMin)
                {
                    return hit;
                }
            }
            else
            {
                if (-e + minBound.X * scale.X > 0.0 || -e + maxBound.X * scale.X < 0.0)
                {
                    return hit;
                }
            }

            // Test intersection with the 2 planes perpendicular to the OBB's Y axis
            Vector3 yaxis = new Vector3(modelMatrix.Column1.X, modelMatrix.Column1.Y, modelMatrix.Column1.Z);
            e = VectorsDot(yaxis, delta);
            f = VectorsDot(ray.Direction, yaxis);

            if (Math.Abs(f) > threshold)
            {
                float t1 = (e + minBound.Y * scale.Y) / f;
                float t2 = (e + maxBound.Y * scale.Y) / f;

                if (t1 > t2)
                {
                    MathHelper.Swap(ref t1, ref t2);
                }
                if (t2 < tMax)
                {
                    tMax = t2;
                }
                if (t1 > tMin)
                {
                    tMin = t1;
                }

                if (tMax < tMin)
                {
                    return hit;
                }
            }
            else
            {
                if (-e + minBound.Y * scale.Y > 0.0 || -e + maxBound.Y * scale.Y < 0.0)
                {
                    return hit;
                }
            }

            // Test intersection with the 2 planes perpendicular to the OBB's Z axis
            Vector3 zaxis = new Vector3(modelMatrix.Column2.X, modelMatrix.Column2.Y, modelMatrix.Column2.Z);
            e = VectorsDot(zaxis, delta);
            f = VectorsDot(ray.Direction, zaxis);

            if (Math.Abs(f) > threshold)
            {
                float t1 = (e + minBound.Z * scale.Z) / f;
                float t2 = (e + maxBound.Z * scale.Z) / f;

                if (t1 > t2)
                {
                    MathHelper.Swap(ref t1, ref t2);
                }
                if (t2 < tMax)
                {
                    tMax = t2;
                }
                if (t1 > tMin)
                {
                    tMin = t1;
                }

                if (tMax < tMin)
                {
                    return hit;
                }
            }
            else
            {
                if (-e + minBound.Z * scale.Z > 0.0 || -e + maxBound.Z * scale.Z < 0.0)
                {
                    return hit;
                }
            }

            minBound = (new Vector4(minBound, 1.0f) * modelMatrix).Xyz;
            maxBound = (new Vector4(maxBound, 1.0f) * modelMatrix).Xyz;
            hit.Distance = tMin;
            hit.Point = new Vector3(
                (minBound.X + maxBound.X) / 2.0f,
                (minBound.Y + maxBound.Y) / 2.0f,
                (minBound.Z + maxBound.Z) / 2.0f);
            return hit;
        }

        private static float VectorsDot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
    }
}
