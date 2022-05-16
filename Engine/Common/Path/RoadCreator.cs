using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Common.Path
{
    class RoadCreatorSettings
    {
        public bool IsClosed = true;

        public float ExpansionAngle = 5;
        public int ExpansionResolution = 10;

        // road mesh
        public float RoadWidth = 1.4f;
        public float ExpansionRoadWidth = 0.8f;
        public float ExpansionSmoothTime = 0.4f;

        // curb mesh
        public float CurbWidth = 0.2f;
        public float MinCurbWidth = 0.02f;
    }

    struct RoadPart
    {
        public Mesh Road;
        public Mesh Curb;
        public Mesh ExitZone;
    }

    static class RoadCreator
    {
        private class AdditionalZone
        {
            private float width = 0.01f;
            private float targetWidth;
            private float velocity;

            public void SmoothWidthIteration(float smoothTime)
            {
                width = SmoothDamp(width, targetWidth, ref velocity, smoothTime);
            }

            public void SetTargetWidth(float value)
            {
                targetWidth = value;
            }

            public float GetWidth() => width;
        }

        private class Curb
        {
            private readonly List<Vector3> vertices = new List<Vector3>();
            private readonly List<Vector2> uvs = new List<Vector2>();
            private readonly List<uint> triangles = new List<uint>();
            private float velocity;

            private float width;
            private float targetWidth;

            private bool needCurb;

            public void SmoothWidthIteration(float smoothTime)
            {
                width = SmoothDamp(width, targetWidth, ref velocity, smoothTime);
            }

            public void SetTargetWidth(float value)
            {
                targetWidth = value;
            }

            public void AddVertices(Vector3 point, Vector3 left, float offset, bool isMirror = false)
            {
                needCurb = true;
                if (isMirror)
                {
                    vertices.Add(point - left * (width + offset));
                    vertices.Add(point - left * offset);
                }
                else
                {
                    vertices.Add(point + left * offset);
                    vertices.Add(point + left * (width + offset));
                }
                
            }

            public void AddUVs(float v)
            {
                if(needCurb) {
                    uvs.Add(new Vector2(0, v));
                    uvs.Add(new Vector2(1, v));
                }
            }

            public void CurbEnd()
            {
                if (triangles.Count > 0 && needCurb)
                {
                    needCurb = false;
                    triangles.RemoveAt(triangles.Count - 1);
                    triangles.RemoveAt(triangles.Count - 1);
                    triangles.RemoveAt(triangles.Count - 1);
                    triangles.RemoveAt(triangles.Count - 1);
                    triangles.RemoveAt(triangles.Count - 1);
                    triangles.RemoveAt(triangles.Count - 1);
                }
            }

            public void AddTriangles(float maxCount)
            {
                if (needCurb && vertices.Count > 0)
                {
                    uint curbVertexIndex = (uint)(vertices.Count - 2);
                    triangles.Add(curbVertexIndex);
                    triangles.Add((uint)((curbVertexIndex + 2) % maxCount));
                    triangles.Add(curbVertexIndex + 1);

                    triangles.Add(curbVertexIndex + 1);
                    triangles.Add((uint)((curbVertexIndex + 2) % maxCount));
                    triangles.Add((uint)((curbVertexIndex + 3) % maxCount));
                }
            }

            public Mesh GetMesh()
            {
                return new Mesh()
                {
                    Vertices = vertices.ToArray(),
                    UVs = uvs.ToArray(),
                    Triangles = triangles.ToArray(),
                };
            }

            public Mesh MergeMesh(Mesh roadMesh)
            {
                for (int i = 0; i < roadMesh.Triangles.Length; i++)
                {
                    roadMesh.Triangles[i] += (uint)vertices.Count;
                }

                return new Mesh()
                {
                    Vertices = vertices.Concat(roadMesh.Vertices).ToArray(),
                    UVs = uvs.Concat(roadMesh.UVs).ToArray(),
                    Triangles = triangles.Concat(roadMesh.Triangles).ToArray(),
                };
            }

            public bool CanStart(float minWidth) => width > minWidth;
        }

        public static RoadPart CreateMesh(Vector3[] points, RoadCreatorSettings creatorSettings)
        {
            // road
            Vector3[] vertices = new Vector3[points.Length * 2];
            Vector2[] uvs = new Vector2[points.Length * 2];
            int triangleCount = (points.Length - 1) * 2 + (creatorSettings.IsClosed ? 2 : 0);
            uint[] triangles = new uint[triangleCount * 3];
            uint vertexIndex = 0;
            uint triangleIndex = 0;

            // additional zone & curb
            var additionalLeftZone = new AdditionalZone();
            var additionalRightZone = new AdditionalZone();
            var leftCurb = new Curb();
            var rightCurb = new Curb();

            for (int i = 0; i < points.Length; i++)
            {
                additionalLeftZone.SmoothWidthIteration(creatorSettings.ExpansionSmoothTime);
                additionalRightZone.SmoothWidthIteration(creatorSettings.ExpansionSmoothTime);
                leftCurb.SmoothWidthIteration(creatorSettings.ExpansionSmoothTime);
                rightCurb.SmoothWidthIteration(creatorSettings.ExpansionSmoothTime);

                Vector3 forward = Vector3.Zero;
                if (i < points.Length - 1 || creatorSettings.IsClosed)
                {
                    forward += points[(i + 1) % points.Length] - points[i];
                }
                if (i > 0 || creatorSettings.IsClosed)
                {
                    forward += points[i] - points[(i - 1 + points.Length) % points.Length];
                    if (i < points.Length - creatorSettings.ExpansionResolution && i % creatorSettings.ExpansionResolution == 0)
                    {
                        Vector3 next = points[(i + creatorSettings.ExpansionResolution) % points.Length] - points[i];
                        float angle = MathHelper.RadiansToDegrees(Vector3.CalculateAngle(forward, next));
                        if (angle > creatorSettings.ExpansionAngle)
                        {
                            Vector3 cross = Vector3.Cross(forward, next);
                            if (cross.Y > 0)
                            {
                                additionalLeftZone.SetTargetWidth(creatorSettings.ExpansionRoadWidth);
                                leftCurb.SetTargetWidth(creatorSettings.CurbWidth);
                            }
                            else
                            {
                                additionalRightZone.SetTargetWidth(creatorSettings.ExpansionRoadWidth);
                                rightCurb.SetTargetWidth(creatorSettings.CurbWidth);
                            }
                        }
                        else
                        {
                            additionalLeftZone.SetTargetWidth(0);
                            additionalRightZone.SetTargetWidth(0);
                            leftCurb.SetTargetWidth(0);
                            rightCurb.SetTargetWidth(0);
                        }
                    }
                }

                forward.Normalize();
                Vector3 left = Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY));

                float roadLeftHalfWidth = (creatorSettings.RoadWidth + additionalLeftZone.GetWidth()) * .5f;
                float roadRightHalfWidth = (creatorSettings.RoadWidth + additionalRightZone.GetWidth()) * .5f;
                vertices[vertexIndex] = points[i] + left * roadLeftHalfWidth;
                vertices[vertexIndex + 1] = points[i] - left * roadRightHalfWidth;

                if (leftCurb.CanStart(creatorSettings.MinCurbWidth)) leftCurb.AddVertices(points[i], left, roadRightHalfWidth, true);
                else leftCurb.CurbEnd();

                if (rightCurb.CanStart(creatorSettings.MinCurbWidth)) rightCurb.AddVertices(points[i], left, roadLeftHalfWidth);
                else rightCurb.CurbEnd();

                float completionPercent = i / (float)(points.Length - 1);
                float v = 1 - Math.Abs(2 * completionPercent - 1);
                uvs[vertexIndex] = new Vector2(0, v);
                uvs[vertexIndex + 1] = new Vector2(1, v);
                leftCurb.AddUVs(v);
                rightCurb.AddUVs(v);

                if (i < points.Length - 1 || creatorSettings.IsClosed)
                {
                    triangles[triangleIndex + 0] = vertexIndex + 0;
                    triangles[triangleIndex + 1] = vertexIndex + 1;
                    triangles[triangleIndex + 2] = (uint)((vertexIndex + 2) % vertices.Length);

                    triangles[triangleIndex + 3] = vertexIndex + 1;
                    triangles[triangleIndex + 4] = (uint)((vertexIndex + 3) % vertices.Length);
                    triangles[triangleIndex + 5] = (uint)((vertexIndex + 2) % vertices.Length);

                    leftCurb.AddTriangles(vertices.Length);
                    rightCurb.AddTriangles(vertices.Length);
                }

                vertexIndex += 2;
                triangleIndex += 6;
            }


            Mesh roadMesh = new Mesh
            {
                Vertices = vertices,
                Triangles = triangles,
                UVs = uvs
            };
            Mesh curbMesh = rightCurb.MergeMesh(leftCurb.GetMesh());
            return new RoadPart
            {
                Road = roadMesh,
                Curb = curbMesh
            };
        }

        public static RoadPart[] CreateMeshes(Vector3[] points, RoadCreatorSettings creatorSettings, int count = 10)
        {
            RoadPart fullRoad = CreateMesh(points, creatorSettings);
            RoadPart[] roadSegments = new RoadPart[count];

            uint[] roadTriangles = fullRoad.Road.Triangles;
            int roadTrianglesPartCount = (roadTriangles.Length / 2 / count) * 2;
            if (roadTrianglesPartCount % 6 != 0) roadTrianglesPartCount = (roadTrianglesPartCount / 4) * 4;

            Vector3[] roadVertices = fullRoad.Road.Vertices;
            int roadVerticesPartCount = roadTrianglesPartCount / 3;

            Vector2[] roadUvs = fullRoad.Road.UVs;
            int roadUvsPartCount = roadTrianglesPartCount / 3;

            for (int i = 0; i < count; i++)
            {
                var roadMesh = new Mesh();
                Vector3[] vertices = roadVertices.SubArray(i * roadVerticesPartCount,
                    (i == count - 1) ? count * roadVerticesPartCount - i * roadVerticesPartCount : roadVerticesPartCount + 2);
                Vector2[] uvs = roadUvs.SubArray(i * roadUvsPartCount,
                    (i == count - 1) ? count * roadUvsPartCount - i * roadUvsPartCount : roadUvsPartCount + 2);
                uint[] triangles = roadTriangles.SubArray(i * roadTrianglesPartCount,
                    (i == count - 1) ? count * roadTrianglesPartCount - i * roadTrianglesPartCount : roadTrianglesPartCount);

                if (i == count - 1 && creatorSettings.IsClosed)
                {
                    vertices = vertices.Concat(roadVertices.SubArray(0, 2)).ToArray();
                    uvs = uvs.Concat(roadUvs.SubArray(0, 2)).ToArray();
                }

                for (int j = 0; j < triangles.Length; j++)
                {
                    triangles[j] -= (uint)(i * roadVerticesPartCount);
                }
                roadMesh.Vertices = vertices;
                roadMesh.Triangles = triangles;
                roadMesh.UVs = uvs;
                roadSegments[i].Road = roadMesh;
            }

            roadSegments[0].Curb = fullRoad.Curb; // TODO: some curb separating as single object maybe
            return roadSegments;
        }

        // Gradually changes a value towards a desired goal over time (by Unity)
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime = 0.3f, float maxSpeed = float.PositiveInfinity)
        {
            float deltaTime = 0.02f;
            smoothTime = Math.Max(0.0001F, smoothTime);
            float omega = 2F / smoothTime;

            float x = omega * deltaTime;
            float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
            float change = current - target;
            float originalTo = target;

            // Clamp maximum speed
            float maxChange = maxSpeed * smoothTime;
            change = MathHelper.Clamp(change, -maxChange, maxChange);
            target = current - change;

            float temp = (currentVelocity + omega * change) * deltaTime;
            currentVelocity = (currentVelocity - omega * temp) * exp;
            float output = target + (change + temp) * exp;

            // Prevent overshooting
            if (originalTo - current > 0.0F == output > originalTo)
            {
                output = originalTo;
                currentVelocity = (output - originalTo) / deltaTime;
            }

            return output;
        }

        private static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }
    }
}