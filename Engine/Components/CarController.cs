using System;
using System.Collections.Generic;
using System.Globalization;
using OpenTK;
using OpenTK.Input;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components
{
    class CarController : Component
    {
        public float ForwardSpeed = 6;
        public float ReverseSpeed = 4;
        public float BreakStrength = 12;
        public float LossSpeed = 2.2f;
        public float TurnStrength = 75;
        public float TurnSpeed = 2;
        public float LossSpeedByTurn = 1.5f;
        public float MaxSpeed = 23;
        public float MinSpeed = -4;
        public Vector3 Forward = Vector3.UnitZ;

        public float CurrentSpeed;
        public float CurrentTurn;
        private float targetTurn;
        private Vector3 position;
        private Vector3 rotation;

        public RennObject Cockpit;
        public RennObject[] ForwardWheels;
        public RennObject[] BackwardWheels;
        private RennObject[] wheelsToMoveRotate;
        private float wheelRotateForward;

        public ParticleEmitter SmokeParticle;
        private float cockpitRotate;
        private float driftMultiplier;
        private float driftFactor;
        private float driftInfluence;
        private bool handBreak;

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

        }

        public override void OnUpdate()
        {
            UpdateVectors();
            Move();
            Rotate();
            FakeDrift();
            RotateWheels();
        }

        private void FakeDrift()
        {
            driftMultiplier = CurrentTurn * CurrentSpeed;
            driftFactor = Math.Abs(driftMultiplier) - MaxSpeed * 0.8f;
            if (driftFactor < 0) driftFactor = 0;
            driftInfluence = driftFactor * 1.3f * (CurrentTurn > 0 ? 1 : -1);
            cockpitRotate = Stabilize(cockpitRotate, 1f, driftInfluence);

            Cockpit.Transform.SetRotation(new Vector3(0, cockpitRotate * 12, 0));
            SmokeParticle.IsPlay = driftFactor > 0 || CurrentSpeed > 5 && handBreak;
            DebugManager.Debug(2, cockpitRotate.ToString(CultureInfo.InvariantCulture));
        }

        private void RotateWheels()
        {
            wheelRotateForward += (float)(CurrentSpeed * Math.PI);
            for (int i = 0; i < wheelsToMoveRotate.Length; i++)
            {
                wheelsToMoveRotate[i].Transform.SetRotation(0, 0, -wheelRotateForward);
            }

            for (int i = 0; i < ForwardWheels.Length; i++)
            {
                ForwardWheels[i].Transform.SetRotation(0, CurrentTurn * 30, 0);
            }
        }

        private void Move()
        {
            KeyboardState input = Keyboard.GetState();

            CurrentSpeed = Stabilize(CurrentSpeed, LossSpeed);

            if (input.IsKeyDown(Key.W))
            {
                CurrentSpeed += ForwardSpeed * Time.DeltaTime;
            }

            if (input.IsKeyDown(Key.S))
            {
                CurrentSpeed -= ReverseSpeed * Time.DeltaTime;
            }

            if (input.IsKeyDown(Key.Space))
            {
                handBreak = true;
                CurrentSpeed = Stabilize(CurrentSpeed, BreakStrength);
            }
            else
            {
                handBreak = false;
            }

            CurrentSpeed = MathHelper.Clamp(CurrentSpeed, MinSpeed, MaxSpeed);
            position += Forward * CurrentSpeed * Time.DeltaTime;
            rennObject.Transform.SetPosition(position);
        }

        private void Rotate()
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.A) && !input.IsKeyDown(Key.D))
            {
                targetTurn = 1;
            }

            if (input.IsKeyDown(Key.D) && !input.IsKeyDown(Key.A))
            {
                targetTurn = -1;
            }

            if (!input.IsKeyDown(Key.D) && !input.IsKeyDown(Key.A))
            {
                targetTurn = 0;
            }
            else if(CurrentSpeed > 0)
            {
                CurrentSpeed -= LossSpeedByTurn * Time.DeltaTime;
            }

            CurrentTurn = Stabilize(CurrentTurn, TurnSpeed, targetTurn);

            float rotateFactor = CalculateRotationFactor() * (CurrentSpeed >= 0 ? 1 : -1);
            rotation += new Vector3(0, CurrentTurn * TurnStrength * Time.DeltaTime * rotateFactor, 0);
            rennObject.Transform.SetRotation(rotation);
        }

        private void UpdateVectors()
        {
            Forward = new Quaternion(0, MathHelper.DegreesToRadians(rotation.Y), 0) * Vector3.UnitZ;
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
            float absSpeed = Math.Abs(CurrentSpeed);
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
            
            return 1 - ((absSpeed - point3) / MaxSpeed);
        }
    }
}
