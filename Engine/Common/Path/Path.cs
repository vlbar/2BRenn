using System;
using System.Collections.Generic;
using OpenTK;

namespace TwoBRenn.Engine.Common.Path
{
    public class Path
    {
        private readonly List<Vector3> points = new List<Vector3>();
        private bool isClosed = false;

        public Vector3 this[int i]
        {
            get => points[i];
        }

        public int PointsCount
        {
            get => points.Count;
        }

        public int SegmentsCount
        {
            get => points.Count / 3;
        }

        public bool IsClosed
        {
            get => isClosed;
            set
            {
                isClosed = value;

                if (value)
                {
                    if ((points.Count + 1) % 3 == 0) points.RemoveAt(points.Count - 1);
                    points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                    points.Add(points[0] * 2 - points[1]);
                }
                else
                {
                    points.RemoveRange(points.Count - 2 , 2);
                }
            }
        }

        public Path()
        {

        }

        public Path(List<Vector3> anchors)
        {
            if (anchors.Count > 2)
            {
                foreach (var anchor in anchors)
                {
                    AddSegment(anchor);
                }
            }
        }

        public void AddSegment(Vector3 anchor)
        {
            if (IsClosed) return;

            if (points.Count == 0)
            {
                points.Add(anchor);
            }
            else if(points.Count == 1)
            {
                points.Add((points[points.Count - 1] + anchor) * .5f);
                points.Add((points[points.Count - 1] + anchor) * .5f);
                points.Add(anchor);
            }
            else
            {
                points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                points.Add((points[points.Count - 1] + anchor) * .5f);
                points.Add(anchor);
            }
        }

        public void AddManualSegment(Vector3 controlPointA, Vector3 anchor, Vector3 controlPointB)
        {
            if (IsClosed) return;

            if (points.Count == 0)
            {
                points.Add(anchor);
                points.Add(controlPointB);
            }
            else
            {
                points.Add(controlPointA);
                points.Add(anchor);
                points.Add(controlPointB);
                MovePoint(points.Count - 1, controlPointB);
            }
        }

        public Vector3[] GetPointsInSegment(int i)
        {
            return new []
            {
                points[i * 3], 
                points[i * 3 + 1], 
                points[i * 3 + 2], 
                points[LoopIndex(i * 3 + 3)]
            };
        }

        public void MovePoint(int i, Vector3 newPosition)
        {
            Vector3 deltaMove = newPosition - points[i];
            points[i] = newPosition;

            if (i % 3 == 0) // move anchor
            {
                // move controls with anchor
                if (i + 1 < points.Count || isClosed)
                {
                    points[LoopIndex(i + 1)] += deltaMove;
                }
                if (i - 1 >= 0 || isClosed)
                {
                    points[LoopIndex(i - 1)] += deltaMove;
                }
            }
            else // move control
            {
                bool nextIsAnchor = (i + 1) % 3 == 0;
                int correspondingControlIndex = nextIsAnchor ? i + 2 : i - 2;

                if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                {
                    Vector3 correspondingControl = points[LoopIndex(correspondingControlIndex)];
                    Vector3 anchor = points[LoopIndex(nextIsAnchor ? i + 1 : i - 1)];

                    float distance = (anchor - correspondingControl).Length;
                    Vector3 direction = (anchor - newPosition).Normalized();
                    points[LoopIndex(correspondingControlIndex)] = anchor + direction * distance;
                }
            }
        }

        public Vector3[] CalculateEvenlySpacedPoints(float spacing, float resolution = 10)
        {
            List<Vector3> evenlySpacedPoints = new List<Vector3>
            {
                points[0]
            };
            Vector3 previousPoint = points[0];
            float distanceSinceLastEvenPoint = 0;

            for (int segmentIndex = 0; segmentIndex < SegmentsCount; segmentIndex++)
            {
                Vector3[] p = GetPointsInSegment(segmentIndex);
                float controlNetLength = Vector3.Distance(p[0], p[1]) + Vector3.Distance(p[1], p[2]) +
                                         Vector3.Distance(p[2], p[3]);
                float estimatedCurveLength = Vector3.Distance(p[0], p[3]) + controlNetLength / 2f;
                int parts = (int)Math.Ceiling(estimatedCurveLength * resolution);
                float t = 0;
                while (t <= 1)
                {
                    t += 1f/parts;
                    Vector3 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                    distanceSinceLastEvenPoint += Vector3.Distance(previousPoint, pointOnCurve);

                    while (distanceSinceLastEvenPoint >= spacing)
                    {
                        float overshootDistance = distanceSinceLastEvenPoint - spacing;
                        Vector3 newEvenlySpacePoint =
                            pointOnCurve + (previousPoint - pointOnCurve).Normalized() * overshootDistance;
                        evenlySpacedPoints.Add(newEvenlySpacePoint);
                        distanceSinceLastEvenPoint = overshootDistance;
                        previousPoint = newEvenlySpacePoint;
                    }

                    previousPoint = pointOnCurve;
                }
            }

            return evenlySpacedPoints.ToArray();
        }

        private int LoopIndex(int i)
        {
            return (i + points.Count) % points.Count;
        }
    }
}