using OpenTK;
using TwoBRenn.Engine.Common.RayCasting;

namespace TwoBRenn.Engine.Components
{
    interface ICollider
    {
        bool IsDynamic { get; set; }

        RaycastHit IntersectWithRay(ref Ray ray);
        bool IntersectWithCollider(ICollider collider, out Vector3 normal);
        RennObject GetOwnerObject();
    }
}
