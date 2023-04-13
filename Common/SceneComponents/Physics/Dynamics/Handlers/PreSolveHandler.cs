using Colin.Common.SceneComponents.Physics.Collision.ContactSystem;
using Colin.Common.SceneComponents.Physics.Collision.Narrowphase;

namespace Colin.Common.SceneComponents.Physics.Dynamics.Handlers
{
    public delegate void PreSolveHandler( Contact contact, ref Manifold oldManifold );
}