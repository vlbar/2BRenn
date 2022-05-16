using System;
using OpenTK;
using TwoBRenn.Engine.Common.RayCasting;

namespace TwoBRenn.Engine.Components.Physic
{
    interface ICollider
    {
        bool IsDynamic { get; set; }
        bool IsTrigger { get; set; }
        Action<IntersectionResult> OnCollisionEnter { get; set; }

        RaycastHit IntersectWithRay(ref Ray ray);
        bool IntersectWithCollider(ICollider collider, out Vector3 normal);
        RennObject GetOwnerObject();
    }
}
