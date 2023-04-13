using Colin.Common.SceneComponents.Physics.Collision.ContactSystem;

namespace Colin.Common.SceneComponents.Physics.Collision.Narrowphase
{
    /// <summary>Used for computing contact manifolds.</summary>
    internal struct ClipVertex
    {
        public ContactId Id;
        public Vector2 V;
    }
}