using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Common.RayCasting;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components
{
    class BoxCollider : Component, ICollider
    {
        public Vector3? MinBound;
        public Vector3? MaxBound;

        private MeshRenderer meshRenderer;

        public override void OnStart()
        {
            if (MinBound == null || MaxBound == null)
            {
                meshRenderer = rennObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null && meshRenderer.Mesh.Vertices.Length > 0)
                {
                    GetBoundsOfMesh(meshRenderer.Mesh);
                }
                else
                {
                    MinBound = -Vector3.One;
                    MaxBound = Vector3.One;
                }
            }

            PhysicsManager.AddCollider(this);
        }

        private void GetBoundsOfMesh(Mesh mesh)
        {
            Vector3 minBound = mesh.Vertices[0];
            Vector3 maxBound = mesh.Vertices[0];

            foreach (var vertex in mesh.Vertices)
            {
                SetMinThat(ref minBound.X, vertex.X);
                SetMinThat(ref minBound.Y, vertex.Y);
                SetMinThat(ref minBound.Z, vertex.Z);

                SetMaxThat(ref maxBound.X, vertex.X);
                SetMaxThat(ref maxBound.Y, vertex.Y);
                SetMaxThat(ref maxBound.Z, vertex.Z);
            }

            MinBound = minBound;
            MaxBound = maxBound;
        }

        private void SetMinThat(ref float a, float b)
        {
            if (a > b) a = b;
        }

        private void SetMaxThat(ref float a, float b)
        {
            if (a < b) a = b;
        }

        public RaycastHit IntersectWithRay(ref Ray ray)
        {
            if (MinBound != null && MaxBound != null)
            {
                Vector3[] bounds = CalculateBoundsWithModel((Vector3)MinBound, (Vector3)MaxBound);
                return Raycast.IntersectionWithBox(ray, bounds[0], bounds[1]);
            }
            return new RaycastHit();
        }

        public RennObject GetOwnerObject()
        {
            return rennObject;
        }

        private Vector3[] CalculateBoundsWithModel(Vector3 minBound, Vector3 maxBound)
        {
            Vector3[] resultVectors = { minBound, maxBound };
            Vector3[] boundVertex = new Vector3[8];
            boundVertex[0] = minBound;
            boundVertex[1] = new Vector3(maxBound.X, minBound.Y, minBound.Z);
            boundVertex[2] = new Vector3(maxBound.X, minBound.Y, maxBound.Z);
            boundVertex[3] = new Vector3(minBound.X, minBound.Y, maxBound.Z);

            boundVertex[4] = maxBound;
            boundVertex[5] = new Vector3(minBound.X, maxBound.Y, maxBound.Z);
            boundVertex[6] = new Vector3(minBound.X, maxBound.Y, minBound.Z);
            boundVertex[7] = new Vector3(maxBound.X, maxBound.Y, minBound.Z);

            for (int i = 0; i < 8; i++)
            {
                Vector4 position = new Vector4(boundVertex[i], 1);
                Vector4 modelPosition = position * rennObject.Transform.GetGlobalModelMatrix();

                SetMinThat(ref resultVectors[0].X, modelPosition.X);
                SetMinThat(ref resultVectors[0].Y, modelPosition.Y);
                SetMinThat(ref resultVectors[0].Z, modelPosition.Z);

                SetMaxThat(ref resultVectors[1].X, modelPosition.X);
                SetMaxThat(ref resultVectors[1].Y, modelPosition.Y);
                SetMaxThat(ref resultVectors[1].Z, modelPosition.Z);
            }

            return resultVectors;
        }
    }
}
