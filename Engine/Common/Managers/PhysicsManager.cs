using System;
using System.Collections.Generic;
using TwoBRenn.Engine.Common.RayCasting;
using TwoBRenn.Engine.Components;

namespace TwoBRenn.Engine.Common.Managers
{
    class PhysicsManager
    {
        private static PhysicsManager _instance;
        public static PhysicsManager Instance => _instance ?? (_instance = new PhysicsManager());

        public List<ICollider> Colliders = new List<ICollider>();

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

        public static void AddCollider(ICollider collider) => Instance.Colliders.Add(collider);
    }
}
