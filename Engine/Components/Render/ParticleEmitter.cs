using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using TwoBRenn.Engine.Common.ParticleSystem;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Render
{
    class ParticleEmitter : Component
    {
        private readonly Random random = new Random();

        public bool IsPlay = true;
        // settings
        public int MaxParticles = 1000;
        public Vector2 LifeTime = new Vector2(10, 10);
        public Vector2 StartSize = Vector2.One;
        public Vector2 SizeVelocity;
        public Vector2 StartRotation = Vector2.Zero;
        public Vector2 RotationVelocity;
        public Color Color = Color.White;
        public Vector4? ColorVelocity;
        public Vector3 Velocity = Vector3.UnitY;
        public Vector3 VelocitySpread;
        public Vector3 EmitRange = Vector3.One;
        public float EmitRate = 5f;

        public Texture Texture;
        public ParticleShader ShaderProgram;

        private float? emitTime;
        private float timeToNextEmit;
        private int lastUsedParticle;

        // render
        private Particle[] particles;
        private float[] particlesOffset;
        private float[] particlesRotateSize;
        private byte[] particlesColor;

        private readonly VertexArrayObject vertexArray = new VertexArrayObject();
        private readonly BufferObject vertexBuffer = new BufferObject(BufferTarget.ArrayBuffer);
        private readonly BufferObject offsetBuffer = new BufferObject(BufferTarget.ArrayBuffer, BufferUsageHint.StreamDraw);
        private readonly BufferObject rotationSizeBuffer = new BufferObject(BufferTarget.ArrayBuffer, BufferUsageHint.StreamDraw);
        private readonly BufferObject colorBuffer = new BufferObject(BufferTarget.ArrayBuffer, BufferUsageHint.StreamDraw);

        private readonly float[] particleVerticesPos = {
            -0.5f, -0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, 0.0f, 1.0f,
            0.5f, -0.5f, 1.0f, 0.0f,
            0.5f, 0.5f,  1.0f, 1.0f,
        };

        public override void OnStart()
        {
            particles = new Particle[MaxParticles];
            particlesOffset = new float[MaxParticles * 3];
            particlesRotateSize = new float[MaxParticles * 2];
            particlesColor = new byte[MaxParticles * 4];
            for (int i = 0; i < MaxParticles; i++)
            {
                particles[i] = new Particle();
            }

            int positionLocation = ShaderProgram.GetAttributeLocation(ParticleShader.VertexPositionAttribute);
            int rotateSizeLocation = ShaderProgram.GetAttributeLocation(ParticleShader.RotationSizeAttribute);
            int offsetLocation = ShaderProgram.GetAttributeLocation(ParticleShader.OffsetAttribute);
            int colorLocation = ShaderProgram.GetAttributeLocation(ParticleShader.BaseColorAttribute);

            vertexArray.Bind();

            vertexBuffer.SetData(particleVerticesPos);
            vertexArray.SetDataPointer(positionLocation, 4);
            vertexArray.SetDivisor(positionLocation, 0);

            rotationSizeBuffer.SetData(particlesRotateSize);
            vertexArray.SetDataPointer(rotateSizeLocation, 2);
            vertexArray.SetDivisor(rotateSizeLocation, 1);

            offsetBuffer.SetData(particlesOffset);
            vertexArray.SetDataPointer(offsetLocation, 3);
            vertexArray.SetDivisor(offsetLocation, 1);

            colorBuffer.SetData(particlesColor);
            vertexArray.SetDataPointer(colorLocation, 4, 0, 0, true, VertexAttribPointerType.UnsignedByte);
            vertexArray.SetDivisor(colorLocation, 1);

            vertexArray.Unbind();
        }

        public override void OnUpdate()
        {
            if (IsPlay) EmitParticles();
            CalculateParticles();
        }

        public override void OnLateUpdate()
        {
            GL.DepthMask(false);

            ShaderProgram.ActiveProgram();
            ShaderProgram.SetMatrix4(BaseShaderProgram.ViewAttribute, Camera.GetViewMatrix());
            ShaderProgram.SetMatrix4(BaseShaderProgram.ProjectionAttribute, Camera.GetProjectionMatrix());

            Texture?.Use();

            offsetBuffer.SetData(particlesOffset);
            rotationSizeBuffer.SetData(particlesRotateSize);
            colorBuffer.SetData(particlesColor);
            vertexArray.DrawArrayInstanced(4, MaxParticles, PrimitiveType.TriangleStrip);

            ShaderProgram.DeactiveProgram();

            GL.DepthMask(true);
        }

        public void Play()
        {
            emitTime = null;
            IsPlay = true;
        }

        public void Play(float time)
        {
            emitTime = time;
            IsPlay = true;
        }

        public void Stop()
        {
            emitTime = null;
            IsPlay = false;
        }

        private void EmitParticles()
        {
            if (emitTime != null)
            {
                emitTime -= Time.DeltaTime;
                if (emitTime < 0) IsPlay = false;
            }

            if (timeToNextEmit <= 0)
            {
                timeToNextEmit = 1;

                int particleIndex = FindUnusedParticle();
                if (particleIndex != -1)
                {
                    SpawnParticle(particleIndex);
                }
            }
            else
            {
                timeToNextEmit -= EmitRate * Time.DeltaTime;
            }
        }

        private int FindUnusedParticle()
        {
            for (int i = lastUsedParticle; i < MaxParticles; i++)
            {
                if (!particles[i].IsLive)
                {
                    lastUsedParticle = i;
                    return i;
                }
            }

            for (int i = 0; i < lastUsedParticle; i++)
            {
                if (!particles[i].IsLive)
                {
                    lastUsedParticle = i;
                    return i;
                }
            }

            lastUsedParticle = 0;
            return -1;
        }

        private void SpawnParticle(int index)
        {
            float lifeTime = RandomFloat(LifeTime.X, LifeTime.Y);
            if (lifeTime <= 0) return;

            float startSize = RandomFloat(StartSize.X, StartSize.Y);
            if (startSize <= 0) return;

            float sizeVelocity = RandomFloat(SizeVelocity.X, SizeVelocity.Y);

            Vector3 center = rennObject.Transform.GetGlobalModelMatrix().ExtractTranslation();
            center.X += RandomFloat(-EmitRange.X * .5f, EmitRange.X * .5f);
            center.Y += RandomFloat(-EmitRange.Y * .5f, EmitRange.Y * .5f);
            center.Z += RandomFloat(-EmitRange.Z * .5f, EmitRange.Z * .5f);

            Vector3 velocity = Velocity;
            if (VelocitySpread != Vector3.Zero)
            {
                velocity.X += RandomFloat(-VelocitySpread.X, VelocitySpread.X);
                velocity.Y += RandomFloat(-VelocitySpread.Y, VelocitySpread.Y);
                velocity.Z += RandomFloat(-VelocitySpread.Z, VelocitySpread.Z);
            }

            float startRotation = RandomFloat(StartRotation.X, StartRotation.Y);
            float rotationVelocity = RandomFloat(RotationVelocity.X, RotationVelocity.Y);

            Particle particle = particles[index];
            particle.ReSpawn(center, startRotation, rotationVelocity, startSize, sizeVelocity, Velocity, Color, ColorVelocity, lifeTime);
        }

        private void CalculateParticles()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Particle particle = particles[i];
                if (particle.IsLive)
                {
                    particle.Update();
                    particlesRotateSize[i * 2 + 0] = MathHelper.DegreesToRadians(particle.Rotation);
                    particlesRotateSize[i * 2 + 1] = particle.Size;
                    particlesOffset[i * 3 + 0] = particle.Position.X;
                    particlesOffset[i * 3 + 1] = particle.Position.Y;
                    particlesOffset[i * 3 + 2] = particle.Position.Z;
                    particlesColor[i * 4 + 0] = particle.Color.R;
                    particlesColor[i * 4 + 1] = particle.Color.G;
                    particlesColor[i * 4 + 2] = particle.Color.B;
                    particlesColor[i * 4 + 3] = particle.Color.A;
                }
                else
                {
                    particlesRotateSize[i * 2 + 1] = 0;
                }
            }
        }

        private float RandomFloat(float min, float max)
        {
            if (Math.Abs(min - max) < 0.01f) return min;
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
