using Colin.Common.Physics.Collision.ContactSystem;

namespace Colin.Common.Physics.Collision.Narrowphase
{
    /// <summary>Used for computing contact manifolds.</summary>
    internal struct ClipVertex
    {
        public ContactId Id;
        public Vector2 V;
    }
}