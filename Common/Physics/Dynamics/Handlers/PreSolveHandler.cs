using Colin.Common.Physics.Collision.ContactSystem;
using Colin.Common.Physics.Collision.Narrowphase;

namespace Colin.Common.Physics.Dynamics.Handlers
{
    public delegate void PreSolveHandler( Contact contact, ref Manifold oldManifold );
}