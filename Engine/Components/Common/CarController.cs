using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Components.Physic;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Common
{
    class CarController : Component
    {
        private Rigidbody rigidbody;

        // forward speed
        public float ForwardSpeed = 8;
        public float ReverseSpeed = 5;
        public float BreakStrength = 12;
        public float LossSpeed = 2.2f;
        public float MaxSpeed = 18;
        public float MinSpeed = -4;
        public Vector3 Forward = Vector3.UnitZ;

        // turning
        public float MagnitudeSpeed;
        public float CurrentSpeed;
        public float CurrentTurn;
        public float TurnStrength = 80;
        public float TurnSpeed = 2;
        public float LossSpeedByTurn = 1.5f;
        private float targetTurn;
        private float handBreakTurn;
        private float handBreakTarget;
        private Vector3 rotation;

        // appearance
        public RennObject Cockpit;
        public RennObject[] ForwardWheels;
        public RennObject[] BackwardWheels;
        private RennObject[] wheelsToMoveRotate;
        private float wheelRotateForward;
        private float cockpitXAngle;

        // drift
        public RennObject CockpitRotationCenter;
        public ParticleEmitter SmokeParticle;
        public float DriftAngle = 7;
        private float cockpitRotate;
        private float driftMultiplier;
        private float driftFactor;
        private float driftInfluence;
        private bool handBreak;

        // others
        private KeyboardState input;
        private Vector3 lastPosition;
        private static Random random = new Random();

        public override void OnStart()
        {
            rotation = rennObject.Transform.rotation;
            List<RennObject> wheelsChild = new List<RennObject>();
            if (ForwardWheels != null)
            {
                foreach (var wheel in ForwardWheels)
                {
                    wheelsChild.AddRange(wheel.ChildObjects);
                }
            }

            if (BackwardWheels != null)
            {
                foreach (var wheel in BackwardWheels)
                {
                    wheelsChild.AddRange(wheel.ChildObjects);
                }
            }

            wheelsToMoveRotate = wheelsChild.ToArray();
            rigidbody = rennObject.GetComponent<Rigidbody>();
        }

        public override void OnUpdate()
        {
            input = Keyboard.GetState();
            UpdateVectors();
            Move();
            Rotate();
            HandBreak();
            FakeDrift();
            RotateWheels();
            MeasureSpeed();
        }

        private void UpdateVectors()
        {
            Forward = new Quaternion(0, MathHelper.DegreesToRadians(rotation.Y), 0) * Vector3.UnitZ;
        }

        private void Move()
        {
            CurrentSpeed = Stabilize(CurrentSpeed, LossSpeed);

            if (input.IsKeyDown(Key.W))
            {
                CurrentSpeed += ForwardSpeed * Time.DeltaTime;
            }

            if (input.IsKeyDown(Key.S))
            {
                CurrentSpeed -= ReverseSpeed * Time.DeltaTime;
                if (MagnitudeSpeed > 8) cockpitXAngle += 15f * Time.DeltaTime;
            }

            if (CurrentSpeed > ReverseSpeed && MagnitudeSpeed < 2f)
            {
                CurrentSpeed = Stabilize(CurrentSpeed, BreakStrength * 2);
            }

            if (MagnitudeSpeed < 5 && MagnitudeSpeed > 1 && input.IsKeyDown(Key.W))
            {
                cockpitXAngle -= 10f * Time.DeltaTime;
            }

            CurrentSpeed = MathHelper.Clamp(CurrentSpeed, MinSpeed, MaxSpeed);
            rigidbody.Force = Forward * CurrentSpeed * 0.02f;
        }

        private void Rotate()
        {
            if (input.IsKeyDown(Key.A) && !input.IsKeyDown(Key.D))
            {
                targetTurn = 1;
            }

            if (input.IsKeyDown(Key.D) && !input.IsKeyDown(Key.A))
            {
                targetTurn = -1;
            }

            if (!input.IsKeyDown(Key.D) && !input.IsKeyDown(Key.A) && !handBreak)
            {
                targetTurn = 0;
            }
            else if (CurrentSpeed > 0)
            {
                CurrentSpeed -= LossSpeedByTurn * Time.DeltaTime;
            }

            CurrentTurn = Stabilize(CurrentTurn, TurnSpeed, targetTurn);

            float rotateFactor = CalculateRotationFactor() * (CurrentSpeed >= 0 ? 1 : -1);
            rotation += new Vector3(0, (CurrentTurn + handBreakTurn) * TurnStrength * Time.DeltaTime * rotateFactor, 0);
            rennObject.Transform.SetRotation(rotation);

            cockpitXAngle = MathHelper.Clamp(cockpitXAngle, -1.5f, 1.5f);
            cockpitXAngle = Stabilize(cockpitXAngle, 5);
            Cockpit.Transform.SetRotation(new Vector3(cockpitXAngle, 0, 0));
        }

        private void HandBreak()
        {
            if (input.IsKeyDown(Key.Space))
            {
                handBreak = true;
                CurrentSpeed = Stabilize(CurrentSpeed, BreakStrength);
                if (MagnitudeSpeed > 8)
                {
                    cockpitXAngle += 35f * Time.DeltaTime;
                    if (handBreakTarget == 0 && targetTurn == 0) handBreakTarget = 0.6f * (random.Next(2) == 0 ? 1 : -1);
                    if (handBreakTarget == 0 && targetTurn != 0) handBreakTarget = targetTurn * 0.5f;
                }
            }
            else
            {
                handBreakTarget = 0;
                handBreak = false;
            }

            handBreakTurn = Stabilize(handBreakTurn, TurnSpeed, handBreakTarget);
        }

        private void FakeDrift()
        {
            driftMultiplier = CurrentTurn * CurrentSpeed;
            driftFactor = Math.Abs(driftMultiplier) - MaxSpeed * 0.8f;
            if (driftFactor < 0) driftFactor = 0;
            driftInfluence = driftFactor * 1.3f * (CurrentTurn > 0 ? 1 : -1);
            cockpitRotate = Stabilize(cockpitRotate, 1f, driftInfluence);

            CockpitRotationCenter.Transform.SetRotation(new Vector3(0, cockpitRotate * DriftAngle, 0));
            SmokeParticle.IsPlay = driftFactor > 0 || CurrentSpeed > 5 && handBreak;
        }

        private void RotateWheels()
        {
            wheelRotateForward += (float)(MagnitudeSpeed * Math.PI);
            foreach (var t in wheelsToMoveRotate)
            {
                t.Transform.SetRotation(0, 0, -wheelRotateForward);
            }

            foreach (var t in ForwardWheels)
            {
                t.Transform.SetRotation(0, CurrentTurn * 30, 0);
            }
        }

        private void MeasureSpeed()
        {
            Vector3 currentPosition = rennObject.Transform.GetGlobalModelMatrix().ExtractTranslation();
            MagnitudeSpeed = Vector3.Distance(currentPosition, lastPosition) / Math.Max(Time.DeltaTime, 0.001f);
            lastPosition = rennObject.Transform.GetGlobalModelMatrix().ExtractTranslation();
            MagnitudeSpeed *= CurrentSpeed < 0 ? -1 : 1;
            DebugManager.Debug(1, MagnitudeSpeed.ToString());
            DebugManager.Debug(2, CurrentSpeed.ToString());
        }


        private float Stabilize(float value, float speed, float target = 0)
        {
            if (value > target)
            {
                value -= speed * Time.DeltaTime;
                if (value < target) value = target;
            }

            if (value < target)
            {
                value += speed * Time.DeltaTime;
                if (value > target) value = target;
            }

            return value;
        }

        private float CalculateRotationFactor()
        {
            float absSpeed = Math.Min(Math.Abs(CurrentSpeed), Math.Abs(MagnitudeSpeed));
            float point2 = MaxSpeed / 4;
            float point3 = MaxSpeed / 2;

            if (absSpeed < point2)
            {
                return absSpeed / point2;
            }

            if (absSpeed < point3)
            {
                return 1;
            }
            
            return 1 - (absSpeed - point3) / MaxSpeed;
        }
    }
}
