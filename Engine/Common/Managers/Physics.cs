using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine.Common.RayCasting;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Components.Physic;

namespace TwoBRenn.Engine.Common.Managers
{
    class Physics
    {
        private static Physics _instance;
        public static Physics Instance => _instance ?? (_instance = new Physics());

        public List<ICollider> Colliders = new List<ICollider>();
        public List<Rigidbody> Rigidbodies = new List<Rigidbody>();

        public static bool Raycast(Ray ray, out RaycastHit hit, float maxDistance = Single.MaxValue)
        {
            hit = new RaycastHit();
            foreach (var collider in Instance.Colliders)
            {
                RaycastHit candidate = collider.IntersectWithRay(ref ray);
                if (candidate.Point != null)
                {
                    if (candidate.HitObject == null || candidate.Distance < hit.Distance && hit.Distance <= maxDistance)
                    {
                        candidate.HitObject = collider.GetOwnerObject();
                        hit = candidate;
                    }
                }
            }
            return hit.Point != null;
        }

        public static bool Intersection(ICollider colliderA, ICollider colliderB, out Vector3 normal)
        {
            normal = Vector3.Zero;
            if (colliderA != colliderB)
            {
                if (colliderA.IntersectWithCollider(colliderB, out var normalSource))
                {
                    normal = normalSource;
                    return true;
                }
            }
            return false;
        }

        public static void AddCollider(ICollider collider) => Instance.Colliders.Add(collider);
        public static void AddRigidbody(Rigidbody rigidbody) => Instance.Rigidbodies.Add(rigidbody);

        public void OnUpdate()
        {
            foreach (var rigidbody in Instance.Rigidbodies)
            {
                ICollider colliderA = rigidbody.Collider;
                foreach (var colliderB in Instance.Colliders)
                {
                    IntersectionResult intersectionResult = new IntersectionResult();
                    for (int i = 0; i < 64; i++)
                    {
                        if (Intersection(colliderA, colliderB, out var normal))
                        {
                            intersectionResult.Normal = normal;
                            if (!colliderB.IsTrigger)
                            {
                                normal.Y = 0;
                                if (rigidbody.Force != Vector3.Zero) normal = rigidbody.Force.Normalized();
                                rigidbody.rennObject.Transform.Translate(-normal * 0.02f);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (intersectionResult.Normal != Vector3.Zero)
                    {
                        intersectionResult.ColliderA = colliderA;
                        intersectionResult.ColliderB = colliderB;
                        intersectionResult.Force = rigidbody.Force;

                        colliderA.OnCollisionEnter?.Invoke(intersectionResult);
                        colliderB.OnCollisionEnter?.Invoke(intersectionResult);

                        if (colliderB.IsDynamic)
                        {
                            colliderB.GetOwnerObject().GetComponent<Rigidbody>().AddForce(rigidbody.Force / 3);
                        }
                    }
                }
            }
        }
    }
}
