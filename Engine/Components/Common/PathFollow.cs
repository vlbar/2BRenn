using System;
using OpenTK;
using TwoBRenn.Engine.Common.Path;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Common
{
    class PathFollow : Component
    {
        public Path Path;
        public float MoveSpeed = 1f;
        public ParticleEmitter DriftParticle;
        public float DriftAngle = 0.016f;

        private int segmentIndex;
        private Vector3[] segmentPoints;
        private float t = 1;
        private float additionalDriftAngle;
        private Vector3 lastPosition = Vector3.Zero;
        private float lastRotation;

        public override void OnUpdate()
        {
            t += MoveSpeed * Time.DeltaTime;
            additionalDriftAngle = MathHelper.Clamp(additionalDriftAngle, -20, 20);
            if (additionalDriftAngle > 0)
            {
                additionalDriftAngle -= 3f * Time.DeltaTime;
                if (additionalDriftAngle < 0) additionalDriftAngle = 0;
            } else if (additionalDriftAngle < 0)
            {
                additionalDriftAngle += 3f * Time.DeltaTime;
                if (additionalDriftAngle > 0) additionalDriftAngle = 0;
            }
            
            if (t >= 1)
            {
                if (segmentIndex < Path.SegmentsCount - 1)
                    segmentIndex++;
                else segmentIndex = 0;
                segmentPoints = Path.GetPointsInSegment(segmentIndex);
                t = 0;
            }

            Vector3 newPosition = Bezier.EvaluateCubic(segmentPoints[0], segmentPoints[1], segmentPoints[2],
                segmentPoints[3], t);
            rennObject.Transform.SetPosition(newPosition);

            Vector3 direction = lastPosition - newPosition;
            direction.Normalize();
            float rotY = (float)Math.Atan2(direction.X, direction.Z);
            rennObject.Transform.SetRotation(0, additionalDriftAngle + MathHelper.RadiansToDegrees(rotY) + 90, 0);
            float delta = Math.Abs(lastRotation - rotY);
            if (delta > DriftAngle)
            {
                if (DriftParticle != null) DriftParticle.Play(1);
                additionalDriftAngle += 12.3f * (lastRotation - rotY > 0 ? -1 : 1) * Time.DeltaTime;
            }
            lastRotation = rotY;

            lastPosition = newPosition;
        }
    }
}