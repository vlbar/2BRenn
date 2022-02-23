using OpenTK;

namespace TwoBRenn.Engine.Core
{
    class Transform
    {
        public Vector3 position { get; private set; } = Vector3.Zero;
        public Vector3 rotation { get; private set; } = Vector3.Zero;
        public Vector3 scale { get; private set; } = Vector3.One;

        private Matrix4 globalModelMatrix = Matrix4.Identity;
        private Matrix4 parentModelMatrix = Matrix4.Identity;

        // matrix
        private Matrix4 GetLocalModelMatrix()
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            Matrix4 rotationY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
            Matrix4 rotationZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            Matrix4 rotationMatrix = rotationY * rotationX * rotationZ;

            Matrix4 translateMatrix = Matrix4.CreateTranslation(position);
            Matrix4 scaleMatrix = Matrix4.CreateScale(scale);

            return translateMatrix * rotationMatrix * scaleMatrix;
        }

        public void UpdateGlobalModel()
        {
            if (parentModelMatrix == Matrix4.Identity)
                globalModelMatrix = GetLocalModelMatrix();
            else
                globalModelMatrix = GetLocalModelMatrix() * parentModelMatrix;
        }

        public Matrix4 GetGlobalModelMatrix() => globalModelMatrix;

        public void SetParentModelMatrix(Matrix4 modelMatrix)
        {
            parentModelMatrix = modelMatrix;
            UpdateGlobalModel();
        }

        // transfrom change
        public void SetPosition(Vector3 vector)
        {
            position = vector;
            UpdateGlobalModel();
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

        // transform overloading
        public void SetPosition(float x, float y, float z)
        {
            SetPosition(new Vector3(x, y, z));
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
