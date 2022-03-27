using OpenTK;

namespace TwoBRenn.Engine.Utils
{
    class Transform
    {
        public Vector3 position { get; private set; } = Vector3.Zero;
        public Vector3 rotation { get; private set; } = Vector3.Zero;
        public Vector3 scale { get; private set; } = Vector3.One;

        private Matrix4 globalModelMatrix = Matrix4.Identity;
        private Transform parentTransform;

        // matrix
        private Matrix4 GetLocalModelMatrix()
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            Matrix4 rotationY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
            Matrix4 rotationZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            Matrix4 rotationMatrix = rotationY * rotationX * rotationZ;

            Matrix4 translateMatrix = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            Matrix4 scaleMatrix = Matrix4.CreateScale(scale);

            return scaleMatrix * rotationMatrix * translateMatrix;
        }

        public void UpdateGlobalModel()
        {
            if (parentTransform == null)
                globalModelMatrix = GetLocalModelMatrix();
            else
                globalModelMatrix = GetLocalModelMatrix() * parentTransform.GetGlobalModelMatrix();
        }

        public Matrix4 GetGlobalModelMatrix() => globalModelMatrix;

        public void SetParentTransform(Transform transform)
        {
            parentTransform = transform;
            UpdateGlobalModel();
        }

        // transfrom change
        public void SetPosition(Vector3 vector)
        {
            position = vector;
            UpdateGlobalModel();
        }

        public void Translate(Vector3 vector)
        {
            position += vector;
            UpdateGlobalModel();
        }

        public void SetGlobalPosition(Vector3 vector)
        {
            if (parentTransform == null)
            {
                SetPosition(vector);
            }
            else
            {
                Matrix4 translateMatrix = Matrix4.CreateTranslation(vector);
                Matrix4 localMatrix = parentTransform.GetGlobalModelMatrix().Inverted() * parentTransform.GetGlobalModelMatrix().ClearTranslation() * translateMatrix;
                position = localMatrix.ExtractTranslation();
                UpdateGlobalModel();
            }
        }

        public void SetRotation(Vector3 vector)
        {
            rotation = vector;
            UpdateGlobalModel();
        }

        public void SetScale(Vector3 vector)
        {
            scale = vector;
            UpdateGlobalModel();
        }

        public Vector3 GetPosition()
        {
            return globalModelMatrix.ExtractTranslation();
        }

        // transform overloading
        public void SetPosition(float x, float y, float z)
        {
            SetPosition(new Vector3(x, y, z));
        }

        public void SetGlobalPosition(float x, float y, float z)
        {
            SetGlobalPosition(new Vector3(x, y, z));
        }

        public void SetRotation(float x, float y, float z)
        {
            SetRotation(new Vector3(x, y, z));
        }

        public void SetScale(float x, float y, float z)
        {
            SetScale(new Vector3(x, y, z));
        }

        public void SetScale(float commonScale)
        {
            SetScale(new Vector3(commonScale, commonScale, commonScale));
        }
    }
}
