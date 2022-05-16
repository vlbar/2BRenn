using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Common.ParticleSystem
{
    class Particle
    {
        private readonly Random random = new Random();

        public Vector3 Position { get; private set; } = Vector3.Zero;
        public float Size { get; private set; }
        public float SizeVelocity { get; private set; }
        public float Rotation { get; private set; }
        public float RotationVelocity { get; private set; }
        public Vector3 Velocity { get; private set; }
        public float LifeTime { get; private set; }
        public Color Color { get; private set; }
        public Vector4? ColorVelocity { get; private set; }
        public bool IsLive { get; private set; }

        private float life;
        private Vector4 floatColor;

        public void ReSpawn(Vector3 position, float rotation, float rotationVelocity, float size, float sizeVelocity, Vector3 velocity, Color color, Vector4? colorVelocity, float lifeTime)
        {
            Position = position;
            Size = size;
            SizeVelocity = sizeVelocity;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            Velocity = velocity;
            Color = color;
            ColorVelocity = colorVelocity;
            LifeTime = lifeTime;

            if (ColorVelocity != null) floatColor = new Vector4(color.R, color.G, color.B, color.A);
            life = LifeTime;
            IsLive = true;
        }

        public void Update()
        {
            if (IsLive)
            {
                life -= Time.DeltaTime;
                if (life < 0)
                {
                    IsLive = false;
                    return;
                }

                float dt = Time.DeltaTime;

                Size += SizeVelocity * dt;
                if (Size <= 0) IsLive = false;

                Position += Velocity * dt;
                Rotation += RotationVelocity * dt;

                if (ColorVelocity != null)
                {
                    floatColor.W += ColorVelocity.Value.W * dt;
                    if (floatColor.W < 0) IsLive = false;

                    floatColor.X += ColorVelocity.Value.X * dt;
                    floatColor.Y += ColorVelocity.Value.Y * dt;
                    floatColor.Z += ColorVelocity.Value.Z * dt;
                    
                    Color = Color.FromArgb(ClampToInt(floatColor.W), ClampToInt(floatColor.X), ClampToInt(floatColor.Y),
                        ClampToInt(floatColor.Z));
                }
            }
        }

        private int ClampToInt(float value, int min = 0, int max = 255)
        {
            int castValue = (int)value;
            if (castValue < min) return min;
            else if (castValue > max) return max;
            else return castValue;
        }

        private float RandomFloat(float min, float max)
        {
            if (Math.Abs(min - max) < 0.01f) return min;
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
