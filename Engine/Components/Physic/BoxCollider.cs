using System;
using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Common.RayCasting;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.Engine.Components.Physic
{
    class BoxCollider : Component, ICollider
    {
        public Vector3 MinBound = Vector3.Zero;
        public Vector3 MaxBound = Vector3.Zero;

        private MeshRenderer meshRenderer;

        // obb
        private Vector3 origin;
        private Vector3 halfSize;

        // circle
        private float radius;

        public bool IsDynamic { get; set; }
        public bool IsTrigger { get; set; } = false;
        public Action<IntersectionResult> OnCollisionEnter { get; set; }

        public override void OnStart()
        {
            ComputeCollisionUtils();
            Physics.AddCollider(this);
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
            return Raycast.IntersectionWithBox(ray, MinBound, MaxBound, rennObject.Transform.GetGlobalModelMatrix());
        }

        public bool IntersectWithCollider(ICollider collider, out Vector3 normal)
        {
            normal = Vector3.Zero;
            if (collider is BoxCollider boxCollider)
            {
                if (IntersectBoxAsCircle(boxCollider) && IntersectionWithBox(boxCollider, out var normalSource))
                {
                    normal = normalSource;
                    return true;
                }
            }

            return false;
        }

        // From BEPUphysics 1 by Ross Nordby
        private bool IntersectionWithBox(BoxCollider collider, out Vector3 normal)
        {
            normal = Vector3.Zero;
            float epsilon = 0.01f;

            Matrix4 transformA = GetOwnerObject().Transform.GetGlobalModelMatrix();
            Matrix4 transformB = collider.GetOwnerObject().Transform.GetGlobalModelMatrix();

            float aX = halfSize.X;
            float aY = halfSize.Y;
            float aZ = halfSize.Z;

            float bX = collider.halfSize.X;
            float bY = collider.halfSize.Y;
            float bZ = collider.halfSize.Z;

            //Relative rotation from A to B.
            Matrix3 bR = Matrix3.Zero;
            Matrix3 aO = Matrix3.CreateFromQuaternion(transformA.ExtractRotation());
            Matrix3 bO = Matrix3.CreateFromQuaternion(transformB.ExtractRotation());

            //Relative translation rotated into A's configuration space.
            Vector3 t = collider.GetGlobalCenter() - GetGlobalCenter();

            bR.M11 = aO.M11 * bO.M11 + aO.M12 * bO.M12 + aO.M13 * bO.M13;
            bR.M12 = aO.M11 * bO.M21 + aO.M12 * bO.M22 + aO.M13 * bO.M23;
            bR.M13 = aO.M11 * bO.M31 + aO.M12 * bO.M32 + aO.M13 * bO.M33;

            Matrix3 absBR = Matrix3.Zero;
            //Epsilons are added to deal with near-parallel edges.
            absBR.M11 = Math.Abs(bR.M11) + epsilon;
            absBR.M12 = Math.Abs(bR.M12) + epsilon;
            absBR.M13 = Math.Abs(bR.M13) + epsilon;
            float tX = t.X;
            t.X = t.X * aO.M11 + t.Y * aO.M12 + t.Z * aO.M13;

            //Test the axes defines by entity A's rotation matrix.
            //A.X
            float rb = bX * absBR.M11 + bY * absBR.M12 + bZ * absBR.M13;
            if (Math.Abs(t.X) > aX + rb)
                return false;
            bR.M21 = aO.M21 * bO.M11 + aO.M22 * bO.M12 + aO.M23 * bO.M13;
            bR.M22 = aO.M21 * bO.M21 + aO.M22 * bO.M22 + aO.M23 * bO.M23;
            bR.M23 = aO.M21 * bO.M31 + aO.M22 * bO.M32 + aO.M23 * bO.M33;
            absBR.M21 = Math.Abs(bR.M21) + epsilon;
            absBR.M22 = Math.Abs(bR.M22) + epsilon;
            absBR.M23 = Math.Abs(bR.M23) + epsilon;
            float tY = t.Y;
            t.Y = tX * aO.M21 + t.Y * aO.M22 + t.Z * aO.M23;

            //A.Y
            rb = bX * absBR.M21 + bY * absBR.M22 + bZ * absBR.M23;
            if (Math.Abs(t.Y) > aY + rb)
                return false;

            bR.M31 = aO.M31 * bO.M11 + aO.M32 * bO.M12 + aO.M33 * bO.M13;
            bR.M32 = aO.M31 * bO.M21 + aO.M32 * bO.M22 + aO.M33 * bO.M23;
            bR.M33 = aO.M31 * bO.M31 + aO.M32 * bO.M32 + aO.M33 * bO.M33;
            absBR.M31 = Math.Abs(bR.M31) + epsilon;
            absBR.M32 = Math.Abs(bR.M32) + epsilon;
            absBR.M33 = Math.Abs(bR.M33) + epsilon;
            t.Z = tX * aO.M31 + tY * aO.M32 + t.Z * aO.M33;

            //A.Z
            rb = bX * absBR.M31 + bY * absBR.M32 + bZ * absBR.M33;
            if (Math.Abs(t.Z) > aZ + rb)
                return false;

            //Test the axes defines by entity B's rotation matrix.
            //B.X
            float ra = aX * absBR.M11 + aY * absBR.M21 + aZ * absBR.M31;
            if (Math.Abs(t.X * bR.M11 + t.Y * bR.M21 + t.Z * bR.M31) > ra + bX)
                return false;

            //B.Y
            ra = aX * absBR.M12 + aY * absBR.M22 + aZ * absBR.M32;
            if (Math.Abs(t.X * bR.M12 + t.Y * bR.M22 + t.Z * bR.M32) > ra + bY)
                return false;

            //B.Z
            ra = aX * absBR.M13 + aY * absBR.M23 + aZ * absBR.M33;
            if (Math.Abs(t.X * bR.M13 + t.Y * bR.M23 + t.Z * bR.M33) > ra + bZ)
                return false;

            //Now for the edge-edge cases.
            //A.X x B.X
            ra = aY * absBR.M31 + aZ * absBR.M21;
            rb = bY * absBR.M13 + bZ * absBR.M12;
            if (absBR.M11 < 1 && Math.Abs(t.Z * bR.M21 - t.Y * bR.M31) > ra + rb)
                return false;

            //A.X x B.Y
            ra = aY * absBR.M32 + aZ * absBR.M22;
            rb = bX * absBR.M13 + bZ * absBR.M11;
            if (absBR.M21 < 1 && Math.Abs(t.Z * bR.M22 - t.Y * bR.M32) > ra + rb)
                return false;

            //A.X x B.Z
            ra = aY * absBR.M33 + aZ * absBR.M23;
            rb = bX * absBR.M12 + bY * absBR.M11;
            if (absBR.M31 < 1 && Math.Abs(t.Z * bR.M23 - t.Y * bR.M33) > ra + rb)
                return false;


            //A.Y x B.X
            ra = aX * absBR.M31 + aZ * absBR.M11;
            rb = bY * absBR.M23 + bZ * absBR.M22;
            if (absBR.M12 < 1 && Math.Abs(t.X * bR.M31 - t.Z * bR.M11) > ra + rb)
                return false;

            //A.Y x B.Y
            ra = aX * absBR.M32 + aZ * absBR.M12;
            rb = bX * absBR.M23 + bZ * absBR.M21;
            if (absBR.M22 < 1 && Math.Abs(t.X * bR.M32 - t.Z * bR.M12) > ra + rb)
                return false;

            //A.Y x B.Z
            ra = aX * absBR.M33 + aZ * absBR.M13;
            rb = bX * absBR.M22 + bY * absBR.M21;
            if (absBR.M32 < 1 && Math.Abs(t.X * bR.M33 - t.Z * bR.M13) > ra + rb)
                return false;

            //A.Z x B.X
            ra = aX * absBR.M21 + aY * absBR.M11;
            rb = bY * absBR.M33 + bZ * absBR.M32;
            if (absBR.M13 < 1 && Math.Abs(t.Y * bR.M11 - t.X * bR.M21) > ra + rb)
                return false;

            //A.Z x B.Y
            ra = aX * absBR.M22 + aY * absBR.M12;
            rb = bX * absBR.M33 + bZ * absBR.M31;
            if (absBR.M23 < 1 && Math.Abs(t.Y * bR.M12 - t.X * bR.M22) > ra + rb)
                return false;

            //A.Z x B.Z
            ra = aX * absBR.M23 + aY * absBR.M13;
            rb = bX * absBR.M32 + bY * absBR.M31;
            if (absBR.M33 < 1 && Math.Abs(t.Y * bR.M13 - t.X * bR.M23) > ra + rb)
                return false;

            normal = t.Normalized();
            return true;
        }

        private bool IntersectBoxAsCircle(BoxCollider collider)
        {
            Vector2 centerA = GetGlobalCenter().Xz;
            Vector2 centerB = collider.GetGlobalCenter().Xz;
            float squaredDistance = Vector2.DistanceSquared(centerA, centerB);
            float squaredRadius = (radius + collider.radius) * (radius + collider.radius);
            return squaredDistance <= squaredRadius;
        }

        public RennObject GetOwnerObject()
        {
            return rennObject;
        }

        private void ComputeCollisionUtils()
        {
            if (MinBound == Vector3.Zero || MaxBound == Vector3.Zero)
            {
                meshRenderer = rennObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null && meshRenderer.Mesh.VerticesArray.Length > 0)
                {
                    GetBoundsOfMesh(meshRenderer.Mesh);
                }
                else
                {
                    MinBound = -Vector3.One;
                    MaxBound = Vector3.One;
                }
            }

            Vector3 scale = GetOwnerObject().Transform.GetGlobalModelMatrix().ExtractScale();
            halfSize = new Vector3(Math.Abs(MinBound.X - MaxBound.X) * scale.X, Math.Abs(MinBound.Y - MaxBound.Y) * scale.Y,
                Math.Abs(MinBound.Z - MaxBound.Z) * scale.Z) * 0.5f;
            origin = new Vector3((MinBound.X + MaxBound.X) * scale.X, (MinBound.Y + MaxBound.Y) * scale.Y,
                (MinBound.Z + MaxBound.Z) * scale.Z) * 0.5f;

            radius = Math.Max(halfSize.X, Math.Max(halfSize.Y, halfSize.Z)) * 1.5f;
        }

        private Vector3 GetGlobalCenter()
        {
            return (new Vector4(origin, 1) * GetOwnerObject().Transform.GetGlobalModelMatrix()).Xyz;
        }
    }
}
