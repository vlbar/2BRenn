using TwoBRenn.Engine.Common.RayCasting;

namespace TwoBRenn.Engine.Components
{
    interface ICollider
    {
        RaycastHit IntersectWithRay(ref Ray ray);
        RennObject GetOwnerObject();
    }
}
